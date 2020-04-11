namespace WunFord.Data.ViewModels.Status
{
    using System;

    public class StatusViewModel : IEquatable<StatusViewModel>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public bool Equals(StatusViewModel other)
        {
            return this.Id == other.Id && this.Name == other.Name;
        }
    }
}
