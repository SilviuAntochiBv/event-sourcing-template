namespace EventBasedServiceTemplate.Domain.Example.Entities
{
    public class SubExampleEntity : IIdentifiable<long>
    {
        public long Id { get; set; }

        public string ChildDescription { get; set; }
    }
}
