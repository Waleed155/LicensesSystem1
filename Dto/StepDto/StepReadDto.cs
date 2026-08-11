namespace Licenses.Dto.StepDto
{
    public class StepReadDto:StepAddDto
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }

    }
}
