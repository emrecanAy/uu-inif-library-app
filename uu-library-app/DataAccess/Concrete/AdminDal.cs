﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uu_library_app.DataAccess.Abstract;
using uu_library_app.Entity.Concrete;
using MySql.Data.MySqlClient;
using uu_library_app.Core.Helpers;

namespace uu_library_app.DataAccess.Concrete
{
    internal class AdminDal : IAdminDal
    {
        MySqlConnection conn = new MySqlConnection(DbConnection.connectionString);

        public void Add(Admin admin)
        {
            try
            {
                conn.Open();
                MySqlCommand commandToAdd = new MySqlCommand("INSERT INTO Admin (id,firstName,lastName,eMail,password) VALUES (@p1, @p2, @p3, @p4,@p5)", conn);
                try
                {
                    commandToAdd.Parameters.AddWithValue("@p1", admin.Id);
                    commandToAdd.Parameters.AddWithValue("@p2", admin.FirstName);
                    commandToAdd.Parameters.AddWithValue("@p3", admin.LastName);
                    commandToAdd.Parameters.AddWithValue("@p4", admin.EMail);
                    commandToAdd.Parameters.AddWithValue("@p5", admin.Password);

                    commandToAdd.ExecuteNonQuery();
                    Console.WriteLine("Başarıyla eklendi!");
                }
                catch (Exception)
                {
                    Console.WriteLine("Hatalı ekleme!");
                }
                conn.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Lütfen internet bağlantınızı kontrol ederek tekrar deneyiniz. Sorunun devam etmesi durumunda bir yetkiliyle iletişime geçiniz.");
            }
           

            
        }

        public void Delete(string id)
        {
            conn.Open();
            try
            {
                MySqlCommand commandToSetDeleted = new MySqlCommand("UPDATE Delete SET deleted=1 WHERE id=@p1 ", conn);
                commandToSetDeleted.Parameters.AddWithValue("@p1", id);
                commandToSetDeleted.ExecuteNonQuery();

            }
            catch (Exception)
            {
                Console.WriteLine("Something went wrong!");
                throw;
            }

            conn.Close();
        }

        List<Admin> admins=new List<Admin>();

        public List<Admin> getAll()
        {
            conn.Open();
            try
            {
                MySqlCommand commandToGetAll = new MySqlCommand("SELECT * FROM Admin WHERE deleted=false", conn);
                MySqlDataReader reader = commandToGetAll.ExecuteReader();
                while (reader.Read())
                {
                    Admin admin = new Admin();
                    admin.Id = reader[0].ToString();
                    admin.FirstName = reader[1].ToString();
                    admin.LastName= reader[2].ToString();
                    admin.EMail = reader[3].ToString();
                    admin.Password = reader[4].ToString();
                    admin.CreatedAt = Convert.ToDateTime(reader[13]);
                    admin.Deleted = Convert.ToBoolean(reader[15]);
                    admins.Add(admin);
                }
                conn.Close();
                return admins;
            }
            catch (Exception)
            {
                Console.WriteLine("Something went wrong!");
                throw;
            }
        }

        public void Update(Admin admin)
        {
            conn.Open();

            MySqlCommand commandToUpdate = new MySqlCommand("UPDATE Admin SET firstName=@p2, lastName=@p3, eMail=@p4, password=@p5 WHERE id=@p1", conn);
            try
            {
                commandToUpdate.Parameters.AddWithValue("@p1", admin.Id);
                commandToUpdate.Parameters.AddWithValue("@p2", admin.FirstName);
                commandToUpdate.Parameters.AddWithValue("@p3", admin.LastName);
                commandToUpdate.Parameters.AddWithValue("@p4", admin.EMail);
                commandToUpdate.Parameters.AddWithValue("@p5", admin.Password);
                commandToUpdate.ExecuteNonQuery();
                Console.WriteLine("Successfully added!");
            }
            catch (Exception)
            {
                Console.WriteLine("Something went wrong!");
                throw;
            }

            conn.Close();
        }

        public Admin getByEmail(string eMail)
        { 
                Admin admin = new Admin();
                conn.Open();
                try
                {
                    MySqlCommand commandToGetAll = new MySqlCommand("SELECT * FROM Admin WHERE eMail=@p1", conn);
                    commandToGetAll.Parameters.AddWithValue("@p1", eMail);
                    MySqlDataReader reader = commandToGetAll.ExecuteReader();
                    while (reader.Read())
                    {
                    admin.id = reader[0].ToString();
                    admin.FirstName = reader[1].ToString();
                    admin.LastName = reader[2].ToString();
                    admin.EMail = reader[3].ToString();
                    admin.Password = reader[4].ToString();
                    }
                    conn.Close();
                    return admin;
                }
                catch (Exception)
                {
                    Console.WriteLine("Something went wrong!");
                    throw;
                }
            
        }

        public Admin GetById(string id)
        {
            Admin admin = new Admin();
            conn.Open();
            try
            {
                MySqlCommand commandToGetAll = new MySqlCommand("SELECT * FROM Admin WHERE id=@p1", conn);
                commandToGetAll.Parameters.AddWithValue("@p1", id);
                MySqlDataReader reader = commandToGetAll.ExecuteReader();
                while (reader.Read())
                {
                    admin.id = reader[0].ToString();
                    admin.FirstName = reader[1].ToString();
                    admin.LastName = reader[2].ToString();
                    admin.EMail = reader[3].ToString();
                    admin.Password = reader[4].ToString();
                }
                conn.Close();
                return admin;
            }
            catch (Exception)
            {
                Console.WriteLine("Something went wrong!");
                throw;
            }
        }
    }
}
