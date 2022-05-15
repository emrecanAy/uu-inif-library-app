﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace uu_library_app.Entity.Concrete
{
    public class Student
    {
        private string id;
        private string departmentId;
        private string firstName;
        private string lastName;
        private string number;
        private string card;
        private string email;
        private DateTime createdAt;
        private bool deleted;

        public Student(string id, string departmentId, string firstName, string lastName, string number, string card, string email, DateTime createdAt)
        {
            this.Id = id;
            this.DepartmentId = departmentId;
            this.FirstName = firstName;
            this.LastName = lastName;
            this.Number = number;
            this.Card = card;
            this.Email = email;
            this.CreatedAt = createdAt;
        }

        public string Id { get => id; set => id = value; }
        public string DepartmentId { get => departmentId; set => departmentId = value; }
        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string Number { get => number; set => number = value; }
        public string Card { get => card; set => card = value; }
        public string Email { get => email; set => email = value; }
        public DateTime CreatedAt { get => createdAt; set => createdAt = value; }
        public bool Deleted { get => deleted; set => deleted = value; }
    }
}