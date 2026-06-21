using System.Threading.RateLimiting;
using Microsoft.AspNetCore.RateLimiting;

namespace Example01.Presentation.Extensions;

internal static class RateLimitingExtensions
{
    private const int RejectionStatusCode = StatusCodes.Status429TooManyRequests;
    
    public static void AddRateLimiting(this WebApplicationBuilder builder)
    {
        var policyType = builder.Configuration.GetRateLimitingPolicyType();

        builder.Services.AddRateLimiter(options =>
        {
            options.RejectionStatusCode = RejectionStatusCode;
            
            options.OnRejected = async (context, cancellationToken) => await HandleRateLimitingRejectionAsync(context, cancellationToken);

            switch (policyType)
            {
                case RateLimitingPolicyType.Fixed:
                    options.AddFixedWindowLimiter();
                    break;
                case RateLimitingPolicyType.Sliding:
                    options.AddSlidingWindowLimiter();
                    break;
                case RateLimitingPolicyType.Concurrency:
                    options.AddConcurrencyLimiter();
                    break;
                case RateLimitingPolicyType.TokenBucket:
                    options.AddTokenBucketLimiter();
                    break;
                case RateLimitingPolicyType.Partition:
                    options.AddPartitionLimiter();
                    break;
                case RateLimitingPolicyType.Chained:
                    options.AddChainedLimiter();
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(policyType), policyType, "Unsupported rate limiting policy type.");
            }
        });
    }
    
    public static RateLimitingPolicyType GetRateLimitingPolicyType(this IConfiguration configuration)
    {
        var policyName = configuration.GetValue<string>("RateLimiting:PolicyName");
        
        if (!Enum.TryParse(policyName, ignoreCase: true, out RateLimitingPolicyType policyType))
        {
            throw new InvalidOperationException($"Invalid rate limiting policy '{policyName}'.");
        }

        return policyType;
    }

    private static void AddFixedWindowLimiter(this RateLimiterOptions options)
    {
        options
            .AddFixedWindowLimiter(policyName: nameof(RateLimitingPolicyType.Fixed), configureOptions =>
            {
                configureOptions.PermitLimit = 3;
                configureOptions.Window = TimeSpan.FromSeconds(10);
                configureOptions.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                configureOptions.AutoReplenishment = true;
                configureOptions.QueueLimit = 1;
            });
    }
    
    private static void AddSlidingWindowLimiter(this RateLimiterOptions options)
    {
        options
            .AddSlidingWindowLimiter(policyName: nameof(RateLimitingPolicyType.Sliding), configureOptions =>
            {
                configureOptions.PermitLimit = 3;
                configureOptions.Window = TimeSpan.FromSeconds(10);
                configureOptions.SegmentsPerWindow = 5;
                configureOptions.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                configureOptions.AutoReplenishment = true;
                configureOptions.QueueLimit = 1;
            });
    }
    
    private static void AddConcurrencyLimiter(this RateLimiterOptions options)
    {
        options
            .AddConcurrencyLimiter(policyName: nameof(RateLimitingPolicyType.Concurrency), configureOptions =>
            {
                configureOptions.PermitLimit = 3;
                configureOptions.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                configureOptions.QueueLimit = 1;
            });
    }
    
    private static void AddTokenBucketLimiter(this RateLimiterOptions options)
    {
        options
            .AddTokenBucketLimiter(policyName: nameof(RateLimitingPolicyType.TokenBucket), configureOptions =>
            {
                configureOptions.TokenLimit = 3;
                configureOptions.ReplenishmentPeriod = TimeSpan.FromSeconds(10);
                configureOptions.TokensPerPeriod = 5;
                configureOptions.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
                configureOptions.AutoReplenishment = true;
                configureOptions.QueueLimit = 1;
            });
    }
    
    private static void AddPartitionLimiter(this RateLimiterOptions options)
    {
        options
            .AddPolicy(policyName: nameof(RateLimitingPolicyType.TokenBucket), context =>
            {
                return RateLimitPartition.GetTokenBucketLimiter(context.Request.Path, _ =>
                    new TokenBucketRateLimiterOptions
                    {
                        TokenLimit = 3,
                        ReplenishmentPeriod = TimeSpan.FromSeconds(10),
                        TokensPerPeriod = 5,
                        QueueProcessingOrder = QueueProcessingOrder.OldestFirst,
                        AutoReplenishment = true,
                        QueueLimit = 1
                    });
            });
    }
    
    private static void AddChainedLimiter(this RateLimiterOptions options)
    {
        options.GlobalLimiter = PartitionedRateLimiter.CreateChained(
            PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
                RateLimitPartition.GetFixedWindowLimiter(httpContext.Request.Method, _ =>
                    new FixedWindowRateLimiterOptions
                    {
                        PermitLimit = 5,
                        Window = TimeSpan.FromSeconds(10),
                        AutoReplenishment = true,
                        QueueLimit = 1
                    })),
            PartitionedRateLimiter.Create<HttpContext, string>(httpContext =>
                RateLimitPartition.GetFixedWindowLimiter(httpContext.Request.Method, _ =>
                    new FixedWindowRateLimiterOptions
                    {
                        PermitLimit = 10,
                        Window = TimeSpan.FromMinutes(1),
                        AutoReplenishment = true,
                        QueueLimit = 1
                    })));
    }
    
    private static async Task HandleRateLimitingRejectionAsync(OnRejectedContext context, CancellationToken cancellationToken)
    {
        var errorMessage = "Too many requests. Please try again";
        
        context.HttpContext.Response.StatusCode = RejectionStatusCode;

        if (context.Lease.TryGetMetadata(MetadataName.RetryAfter, out var retryAfter))
        {
            errorMessage = $"{errorMessage} after {retryAfter.TotalSeconds} second(s). ";
        }

        await context.HttpContext.Response.WriteAsync(errorMessage, cancellationToken);
    }
}

internal enum RateLimitingPolicyType
{
    Fixed = 1,
    Sliding,
    Concurrency,
    TokenBucket,
    Partition,
    Chained
}