namespace DotNeet.Core.Tests.Cpu

open NUnit.Framework

[<TestFixture>]
type Cpu8086Tests () =

    [<SetUp>]
    member this.SetUp() =
        Memory.zero
