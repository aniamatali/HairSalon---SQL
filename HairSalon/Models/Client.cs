using System.Collections.Generic;
using MySql.Data.MySqlClient;
using System;

namespace HairSalon.Models
{
    public class Client
    {
        private string _name;
        private int _id;
        private int _stylistId;
        private string _hours;

        public Client(string name, int stylistId, string hours, int id = 0)
        {
            _name = name;
            _stylistId = stylistId;
            _id = id;
            _hours = hours;
        }

        public override bool Equals(System.Object otherClient)
        {
          if (!(otherClient is Client))
          {
            return false;
          }
          else
          {
             Client newClient = (Client) otherClient;
             bool idEquality = this.GetId() == newClient.GetId();
             bool nameEquality = this.GetName() == newClient.GetName();
             bool stylistEquality = this.GetstylistId() == newClient.GetstylistId();
             bool hoursEquality = this.GetHours() == newClient.GetHours();
             return (idEquality && nameEquality && stylistEquality && hoursEquality);
           }
        }
        public override int GetHashCode()
        {
             return this.GetName().GetHashCode();
        }

        public string GetName()
        {
            return _name;
        }
        public int GetId()
        {
            return _id;
        }
        public int GetstylistId()
        {
            return _stylistId;
        }

        public string GetHours()
        {
            return _hours;
        }

        public void Save()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();

            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"INSERT INTO clients (name, stylist_id, hours) VALUES (@name, @stylist_id, @hours);";

            MySqlParameter name = new MySqlParameter();
            name.ParameterName = "@name";
            name.Value = this._name;
            cmd.Parameters.Add(name);

            MySqlParameter stylistId = new MySqlParameter();
            stylistId.ParameterName = "@stylist_id";
            stylistId.Value = this._stylistId;
            cmd.Parameters.Add(stylistId);

            MySqlParameter hours = new MySqlParameter();
            hours.ParameterName = "@hours";
            hours.Value = this._hours;
            cmd.Parameters.Add(hours);


            cmd.ExecuteNonQuery();
            _id = (int) cmd.LastInsertedId;
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static List<Client> GetAll()
        {
            List<Client> allClients = new List<Client> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM clients ORDER BY hours;";
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
              int clientId = rdr.GetInt32(0);
              string clientname = rdr.GetString(1);
              int clientstylistId = rdr.GetInt32(2);
              string clienthours = rdr.GetString(3);
              Client newClient = new Client(clientname, clientstylistId, clienthours, clientId);
              allClients.Add(newClient);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allClients;
        }
        public static Client Find(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM clients WHERE id = (@searchId);";

            MySqlParameter searchId = new MySqlParameter();
            searchId.ParameterName = "@searchId";
            searchId.Value = id;
            cmd.Parameters.Add(searchId);

            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            int clientId = 0;
            string clientName = "";
            string clienthours = "";
            int clientstylistId = 0;

            while(rdr.Read())
            {
              clientId = rdr.GetInt32(0);
              clientName = rdr.GetString(1);
              clientstylistId = rdr.GetInt32(2);
              clienthours = rdr.GetString(3);
            }
            Client newClient = new Client(clientName, clientstylistId, clienthours, clientId);
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return newClient;
        }

        public static void DeleteAll()
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM clients;";
            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static void DeleteClients(int id)
        {
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"DELETE FROM clients WHERE stylist_id = @id;";

            MySqlParameter cuisineId = new MySqlParameter();
            cuisineId.ParameterName = "@id";
            cuisineId.Value = id;
            cmd.Parameters.Add(cuisineId);

            cmd.ExecuteNonQuery();
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
        }

        public static List<Client> GetAlphaList()
        {
            List<Client> allClients = new List<Client> {};
            MySqlConnection conn = DB.Connection();
            conn.Open();
            var cmd = conn.CreateCommand() as MySqlCommand;
            cmd.CommandText = @"SELECT * FROM clients ORDER BY hours;";
            var rdr = cmd.ExecuteReader() as MySqlDataReader;
            while(rdr.Read())
            {
              int clientId = rdr.GetInt32(0);
              string clientname = rdr.GetString(1);
              int clientstylistId = rdr.GetInt32(2);
              string clienthours = rdr.GetString(3);
              Client newClient = new Client(clientname, clientstylistId, clienthours, clientId);
              allClients.Add(newClient);
            }
            conn.Close();
            if (conn != null)
            {
                conn.Dispose();
            }
            return allClients;
        }

    }
}
