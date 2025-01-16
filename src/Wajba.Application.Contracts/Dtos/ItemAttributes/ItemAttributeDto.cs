namespace Wajba.Dtos.ItemAttributes
{
    public class ItemAttributeDto : EntityDto<int>
    {
        public string Name { get; set; }
        public int Status { get; set; }
    }
}
