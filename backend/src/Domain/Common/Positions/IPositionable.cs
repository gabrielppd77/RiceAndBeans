namespace Domain.Common.Positions;

public interface IPositionable
{
    int Position { get; }
    void ChangePosition(int newPosition);
}