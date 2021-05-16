namespace Plannial.Core.Models.Entities
{
    public class Grade
    {
        private string _value;

        public int Id { get; set; }
        public string Value
        {
            get => _value; set
            {
                _value = value.ToUpper();
            }
        }
    }
}