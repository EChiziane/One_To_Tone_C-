namespace Domain
{
    public class Address
    {
        public int  Id { get; set; }
        public  string Place { get; set; }
        public Person Person { get; set; }
    }
}