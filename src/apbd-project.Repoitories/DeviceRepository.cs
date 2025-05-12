using Entities;
using Microsoft.Data.SqlClient;
using System.Data;
using Entities.DTO_s;

namespace Repository;

public class DeviceRepository : IDeviceRepository
{
    private readonly string _connectionString;

    public DeviceRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public IEnumerable<object> GetAllDevices()
    {
        List<object> devices = [];

        const string sql = "SELECT Id, Name, IsEnabled FROM Device";

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            SqlCommand command = new SqlCommand(sql, connection);

            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            try
            {
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        devices.Add(new
                        {
                            Id = reader.GetString(0),
                            Name = reader.GetString(1),
                            IsEnabled = reader.GetBoolean(2)
                        });
                    }
                }
            }
            finally
            {
                reader.Close();
            }
        }

        return devices;
    }

    public object? GetDeviceById(string id)
    {
        object? device = null;

        const string sql = @"
        SELECT d.Id, d.Name, d.IsEnabled, d.RV, 
               e.IpAddress, e.NetworkName, 
               sw.BatteryLevel, 
               pc.OperatingSystem
        FROM Device d
        LEFT JOIN Embedded e ON d.Id = e.DeviceId
        LEFT JOIN Smartwatch sw ON d.Id = sw.DeviceId
        LEFT JOIN PersonalComputer pc ON d.Id = pc.DeviceId
        WHERE d.Id = @Id";

        using SqlConnection connection = new SqlConnection(_connectionString);
        SqlCommand command = new SqlCommand(sql, connection);
        command.Parameters.AddWithValue("@Id", id);

        connection.Open();
        SqlDataReader reader = command.ExecuteReader();
        try
        {
            if (reader.Read())
            {

                if (id.StartsWith("SW-"))
                {
                    device = new SmartwatchDTO(
                        reader.GetString(0),
                        reader.GetString(1), 
                        reader.GetBoolean(2), 
                        reader.IsDBNull(6) ? 0 : reader.GetInt32(6));
                }
                else if (id.StartsWith("E-"))
                {
                    device = new EmbeddedDTO(
                        reader.GetString(0), 
                        reader.GetString(1), 
                        reader.GetBoolean(2), 
                        reader.IsDBNull(4) ? "" : reader.GetString(4), 
                        reader.IsDBNull(5) ? "" : reader.GetString(5));
                }
                else if (id.StartsWith("P-"))
                {
                    device = new PCDTO(
                        reader.GetString(0), 
                        reader.GetString(1), 
                        reader.GetBoolean(2), 
                        reader.IsDBNull(7) ? null : reader.GetString(7));
                }
            }
        }
        finally
        {
            reader.Close();
        }
        
        return device;
    }

    public bool AddDevice(Device device)
    {
        int countRowsAdded = -1;

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            SqlTransaction transaction = connection.BeginTransaction();

            try
            {
                SqlCommand command;

                if (device is Smartwatch sw)
                {
                    command = new SqlCommand("AddSmartwatch", connection, transaction);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@DeviceId", sw.Id);
                    command.Parameters.AddWithValue("@Name", sw.Name);
                    command.Parameters.AddWithValue("@IsEnabled", sw.IsEnabled);
                    command.Parameters.AddWithValue("@BatteryPercentage", sw.BatteryLevel);
                }
                else if (device is PersonalComputer pc)
                {
                    command = new SqlCommand("AddPersonalComputer", connection, transaction);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@DeviceId", pc.Id);
                    command.Parameters.AddWithValue("@Name", pc.Name);
                    command.Parameters.AddWithValue("@IsEnabled", pc.IsEnabled);
                    command.Parameters.AddWithValue("@OperatingSystem", (object?)pc.OperatingSystem ?? DBNull.Value);
                }
                else if (device is Embedded e)
                {
                    command = new SqlCommand("AddEmbedded", connection, transaction);
                    command.CommandType = CommandType.StoredProcedure;

                    command.Parameters.AddWithValue("@DeviceId", e.Id);
                    command.Parameters.AddWithValue("@Name", e.Name);
                    command.Parameters.AddWithValue("@IsEnabled", e.IsEnabled);
                    command.Parameters.AddWithValue("@IpAddress", e.IpAddress);
                    command.Parameters.AddWithValue("@NetworkName", e.NetworkName);
                }
                else
                {
                    return false;
                }

                countRowsAdded = command.ExecuteNonQuery();
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                return false;
            }
        }

        return countRowsAdded != -1;
    }


    public bool UpdateDevice(Device device)
    {
        int affectedRows = -1;

        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            connection.Open();
            SqlTransaction transaction = connection.BeginTransaction();

            try
            {
                const string updateSql = @"
                UPDATE Device 
                SET Name = @Name, IsEnabled = @IsEnabled 
                WHERE Id = @Id";

                Console.WriteLine(device.Id);

                SqlCommand command = new SqlCommand(updateSql, connection, transaction);
                command.Parameters.AddWithValue("@Id", device.Id);
                command.Parameters.AddWithValue("@Name", device.Name);
                command.Parameters.AddWithValue("@IsEnabled", device.IsEnabled);

                affectedRows = command.ExecuteNonQuery();
                if (affectedRows == 0)
                {
                    transaction.Rollback();
                    return false;
                }

                string deleteSql = device switch
                {
                    Smartwatch => "DELETE FROM Smartwatch WHERE DeviceId = @DeviceId",
                    Embedded => "DELETE FROM Embedded WHERE DeviceId = @DeviceId",
                    PersonalComputer => "DELETE FROM PersonalComputer WHERE DeviceId = @DeviceId",
                    _ => throw new Exception("Unknown device type")
                };

                command = new SqlCommand(deleteSql, connection, transaction);
                command.Parameters.AddWithValue("@DeviceId", device.Id);
                command.ExecuteNonQuery();

                if (device is Smartwatch sw)
                {
                    command = new SqlCommand(
                        "INSERT INTO Smartwatch (BatteryLevel, DeviceId) VALUES (@BatteryLevel, @DeviceId)", connection,
                        transaction);
                    command.Parameters.AddWithValue("@BatteryLevel", sw.BatteryLevel);
                }
                else if (device is Embedded e)
                {
                    command = new SqlCommand(
                        "INSERT INTO Embedded (IpAddress, NetworkName, DeviceId) VALUES (@IpAddress, @NetworkName, @DeviceId)",
                        connection, transaction);
                    command.Parameters.AddWithValue("@IpAddress", e.IpAddress);
                    command.Parameters.AddWithValue("@NetworkName", e.NetworkName);
                }
                else if (device is PersonalComputer pc)
                {
                    command = new SqlCommand(
                        "INSERT INTO PersonalComputer (OperatingSystem, DeviceId) VALUES (@OperatingSystem, @DeviceId)",
                        connection, transaction);
                    command.Parameters.AddWithValue("@OperatingSystem", (object?)pc.OperatingSystem ?? DBNull.Value);
                }

                command.Parameters.AddWithValue("@DeviceId", device.Id);
                command.ExecuteNonQuery();

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                return false;
            }
        }

        return affectedRows != -1;
    }

    public bool DeleteDevice(string Id)
    {
        using SqlConnection connection = new SqlConnection(_connectionString);
        connection.Open();

        SqlTransaction transaction = connection.BeginTransaction();

        try
        {
            SqlCommand command = new SqlCommand("DELETE FROM Device WHERE Id = @Id", connection, transaction);
            command.Parameters.AddWithValue("@Id", Id);

            int rows = command.ExecuteNonQuery();
            transaction.Commit();

            return rows > 0;
        }
        catch
        {
            transaction.Rollback();
            return false;
        }
    }
}