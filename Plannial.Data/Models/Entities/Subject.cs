namespace Plannial.Data.Models.Entities
{
    public class Subject : BaseOwnedEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public Grade Grade { get; set; }
    }
}
