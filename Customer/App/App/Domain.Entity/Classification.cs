namespace App.Domain.Entity
{
    public enum Classification
    {
        //ToDO needs default to stop failure if classification not the same set of values from other sources eg database
        //Maybe replacing enums with a look up maybe configuation or service
        Bronze = 1,
        Silver = 2,
        Gold = 3
    }
}