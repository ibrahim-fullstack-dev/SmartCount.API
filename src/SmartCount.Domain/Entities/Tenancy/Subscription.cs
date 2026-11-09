
using SmartCount.Domain.Entities.Common;
using SmartCount.Domain.Enums;

namespace SmartCount.Domain.Entities.Tenancy;

public class Subscription : AuditableEntity
{
    public int TenantId { get; private set; }

    public int PlanId { get; private set; }

    public int CurrencyId { get; private set; }

    public SubscriptionStatus Status { get; private set; }

    public decimal Price { get; private set; }

    public BillingCycle BillingCycle { get; private set; }

    public DateTime StartDate { get; private set; }

    public DateTime CurrentPeriodStart { get; private set; }

    public DateTime CurrentPeriodEnd { get; private set; }

    public DateTime? TrialEndsOn { get; private set; }

    public bool CancelAtPeriodEnd { get; private set; }

    public DateTime? CancelledOn { get; private set; }

    private Subscription()
    {
    }

    private Subscription(int tenantId,int planId,int currencyId,decimal price,BillingCycle billingCycle,DateTime startDate,DateTime currentPeriodStart,DateTime currentPeriodEnd,DateTime? trialEndsOn)
    {
        TenantId = tenantId;
        PlanId = planId;
        CurrencyId = currencyId;
        Price = price;
        BillingCycle = billingCycle;
        StartDate = startDate;
        CurrentPeriodStart = currentPeriodStart;
        CurrentPeriodEnd = currentPeriodEnd;
        TrialEndsOn = trialEndsOn;

        Status = trialEndsOn.HasValue
            ? SubscriptionStatus.Trialing
            : SubscriptionStatus.Active;

        CancelAtPeriodEnd = false;
    }

    public static Subscription Create(int tenantId,int planId,int currencyId,decimal price,BillingCycle billingCycle,DateTime startDate,DateTime currentPeriodStart,DateTime currentPeriodEnd,DateTime? trialEndsOn = null)
    {
        Validate(tenantId,planId,currencyId,price,billingCycle,startDate,currentPeriodStart,currentPeriodEnd,trialEndsOn);       
        return new Subscription(tenantId,planId,currencyId,price,billingCycle,startDate,currentPeriodStart,currentPeriodEnd,trialEndsOn);
    }

    public void RequestCancellation()
    {
        if (Status is SubscriptionStatus.Cancelled
            or SubscriptionStatus.Expired)
        {
            throw new InvalidOperationException(
                "Subscription cannot be cancelled in its current state.");
        }

        CancelAtPeriodEnd = true;
    }

    public void Resume()
    {
        if (Status is SubscriptionStatus.Cancelled
            or SubscriptionStatus.Expired)
        {
            throw new InvalidOperationException(
                "Subscription cannot be resumed.");
        }

        CancelAtPeriodEnd = false;
    }

    public void Cancel(DateTime cancelledOn)
    {
        if (Status is SubscriptionStatus.Cancelled
            or SubscriptionStatus.Expired)
        {
            throw new InvalidOperationException(
                "Subscription is already terminated.");
        }

        if (cancelledOn == default)
            throw new ArgumentException(
                "Cancellation date is required.",
                nameof(cancelledOn));

        Status = SubscriptionStatus.Cancelled;
        CancelledOn = cancelledOn;
        CancelAtPeriodEnd = false;
    }

    public void MarkPastDue()
    {
        if (Status != SubscriptionStatus.Active)
        {
            throw new InvalidOperationException(
                "Only active subscriptions can become past due.");
        }

        Status = SubscriptionStatus.PastDue;
    }

    public void Activate()
    {
        if (Status is not (
            SubscriptionStatus.Trialing
            or SubscriptionStatus.PastDue))
        {
            throw new InvalidOperationException(
                "Subscription cannot be activated from its current state.");
        }

        Status = SubscriptionStatus.Active;
    }

    public void RenewPeriod(DateTime periodStart,DateTime periodEnd)
    {
        if (Status is SubscriptionStatus.Cancelled
            or SubscriptionStatus.Expired)
        {
            throw new InvalidOperationException(
                "Terminated subscriptions cannot be renewed.");
        }

        if (periodStart.Date != CurrentPeriodEnd.Date)
        {
            throw new ArgumentException(
                "New period must start at the end of the current period.",
                nameof(periodStart));
        }

        if (periodEnd.Date <= periodStart.Date)
        {
            throw new ArgumentException(
                "Period end must be after period start.",
                nameof(periodEnd));
        }

        if (CancelAtPeriodEnd)
        {
            Cancel(periodStart);
            return;
        }

        CurrentPeriodStart = periodStart;
        CurrentPeriodEnd = periodEnd;
        Status = SubscriptionStatus.Active;
        TrialEndsOn = null;
    }

    public void Expire()
    {
        if (Status is SubscriptionStatus.Cancelled
            or SubscriptionStatus.Expired)
        {
            throw new InvalidOperationException(
                "Subscription is already terminated.");
        }

        Status = SubscriptionStatus.Expired;
    }

    private static void Validate(int tenantId,int planId,int currencyId,decimal price,BillingCycle billingCycle,DateTime startDate,DateTime currentPeriodStart,DateTime currentPeriodEnd,DateTime? trialEndsOn)
    {
        if (tenantId <= 0)
            throw new ArgumentOutOfRangeException(nameof(tenantId));

        if (planId <= 0)
            throw new ArgumentOutOfRangeException(nameof(planId));

        if (currencyId <= 0)
            throw new ArgumentOutOfRangeException(nameof(currencyId));

        if (price < 0)
            throw new ArgumentOutOfRangeException(nameof(price));

        if (!Enum.IsDefined(billingCycle))
            throw new ArgumentOutOfRangeException(nameof(billingCycle));

        if (startDate == default)
            throw new ArgumentException("Start date is required.");

        if (currentPeriodStart < startDate)
            throw new ArgumentException(
                "Current period cannot start before subscription start.");

        if (currentPeriodEnd <= currentPeriodStart)
            throw new ArgumentException(
                "Current period end must be after its start.");

        if (trialEndsOn.HasValue &&
            trialEndsOn.Value <= startDate)
        {
            throw new ArgumentException(
                "Trial end must be after subscription start.",
                nameof(trialEndsOn));
        }
    }
}
