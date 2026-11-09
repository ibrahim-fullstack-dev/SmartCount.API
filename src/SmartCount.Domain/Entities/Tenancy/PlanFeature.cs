
using SmartCount.Domain.Entities.System;

namespace SmartCount.Domain.Entities.Tenancy;

public class PlanFeature
{
    public int Id { get; private set; }

    public int PlanId { get; private set; }

    public int FeatureId { get; private set; }

    public bool IsEnabled { get; private set; }

    public int? LimitValue { get; private set; }

    public Plan Plan { get; private set; } = null!;

    public Feature Feature { get; private set; } = null!;

    private PlanFeature()
    {
    }

    private PlanFeature(int planId,int featureId,bool isEnabled,int? limitValue)
    {
        PlanId = planId;
        FeatureId = featureId;
        IsEnabled = isEnabled;
        LimitValue = limitValue;
    }

    public static PlanFeature Create(int planId,int featureId,bool isEnabled,int? limitValue = null)
    {
        Validate(planId, featureId, isEnabled, limitValue);

        return new PlanFeature(planId,featureId,isEnabled,limitValue);
    }

    public void UpdateSettings(bool isEnabled,int? limitValue = null)
    {
        Validate(PlanId, FeatureId, isEnabled, limitValue);

        IsEnabled = isEnabled;
        LimitValue = limitValue;
    }

    private static void Validate(int planId,int featureId,bool isEnabled,int? limitValue)
    {
        if (planId <= 0)
            throw new ArgumentOutOfRangeException(nameof(planId));

        if (featureId <= 0)
            throw new ArgumentOutOfRangeException(nameof(featureId));

        if (limitValue < 0)
            throw new ArgumentOutOfRangeException(nameof(limitValue));

        if (!isEnabled && limitValue.HasValue)
            throw new ArgumentException(
                "A disabled feature cannot have a limit value.",
                nameof(limitValue));
    }
}
