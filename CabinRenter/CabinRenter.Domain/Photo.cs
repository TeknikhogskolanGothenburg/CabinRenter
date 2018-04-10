namespace CabinRenter.Domain
{
    public class Photo
    {
        public int Id { get; set; }
        public string Path { get; set; }
        public string Title { get; set; }
        public int Order { get; set; }
        public RentalObject RentalObject { get; set; }
        public int RentalObjectId { get; set; }

    }
}