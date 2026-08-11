namespace Licenses.Dto.StageDto
{
    public class StageReadDto:StageAddDto
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
    }
}
