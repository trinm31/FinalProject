namespace Management.Services.Dtos;

public class ScheduleDto
{
    public DateTime FirstDay { get; set; }
    public List<Events> Events { get; set; }
}