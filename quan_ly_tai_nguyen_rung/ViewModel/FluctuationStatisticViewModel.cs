public class FluctuationStatisticViewModel
{
    public string FacilityName { get; set; }
    public int FacilityId { get; set; }
    public List<YearlyFluctuationStatistics> YearlyData { get; set; }
}

public class YearlyFluctuationStatistics
{
    public int Year { get; set; }
    public List<QuarterlyFluctuationStatistics> QuarterlyData { get; set; }
}

public class QuarterlyFluctuationStatistics
{
    public int Quarter { get; set; }
    public List<MonthlyFluctuationStatistics> MonthlyData { get; set; }
}

public class MonthlyFluctuationStatistics
{
    public int Month { get; set; }
    public int TotalQuantityChange { get; set; }
    public List<FluctuationDetails> FluctuationDetails { get; set; }
}

public class FluctuationDetails
{
    public string AnimalName { get; set; }
    public int QuantityChange { get; set; }
    public List<string> Reasons { get; set; }
}
