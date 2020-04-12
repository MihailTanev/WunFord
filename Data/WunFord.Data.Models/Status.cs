namespace WunFord.Data.Models
{
    using System;

    public class Status : IEquatable<Status>
    {       
        public int Id { get; set; }

        public string Name { get; set; }

        public bool Equals(Status other)
        {
            return this.Id == other.Id && this.Name == other.Name;
        }
    }
}