namespace DotNeet.Core.Tests.Memory

open NUnit.Framework

[<TestFixture>]
type MemoryTests () =

    [<SetUp>]
    member this.SetUp() =
        Memory.zero

    [<Test>]
    member this.CanWriteSingleBytes() =
        Memory.writeByte 0 0xAAuy
        Memory.writeByte 1 0xBBuy
        Memory.writeByte 2 0xCCuy
        Memory.writeByte 3 0xDDuy

        let byteValue = Memory.readByte 0
        let shortValue = Memory.readShort 0
        let intValue = Memory.readInt 0

        Assert.That(byteValue, Is.EqualTo(0xAAu))
        Assert.That(shortValue, Is.EqualTo(0xBBAAu))
        Assert.That(intValue, Is.EqualTo(0xDDCC_BBAAu))

    [<Test>]
    member this.CanWriteMultipleBytes() =
        let data = [| 0x11uy; 0x22uy; 0x33uy; 0x44uy |]
        Memory.writeBytes 0 data

        let byteValue = Memory.readByte 0
        let shortValue = Memory.readShort 0
        let intValue = Memory.readInt 0

        Assert.That(byteValue, Is.EqualTo(0x11u))
        Assert.That(shortValue, Is.EqualTo(0x2211u))
        Assert.That(intValue, Is.EqualTo(0x4433_2211u))