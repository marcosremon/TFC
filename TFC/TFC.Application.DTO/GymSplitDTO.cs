namespace TFC.Application.DTO
{
    public class GymSplitDTO
    {
        public long GymSplitId { get; set; }
        public string? Name { get; set; }
        public List<SplitDayDTO>? Days { get; set; }
    }
}