using Xunit;

// This file exists to disable xUnit parallelization for the entire functional test assembly.
// These tests share a static payment-token counter in BaseTest and the same sandbox resources,
// so running them concurrently makes token selection non-deterministic and causes flaky failures.
// The trade-off is longer execution time: the suite grows from roughly 30 seconds to ~100 seconds.
[assembly: CollectionBehavior(DisableTestParallelization = true)]
