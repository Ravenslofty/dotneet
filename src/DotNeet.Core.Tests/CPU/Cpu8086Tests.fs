namespace DotNeet.Core.Tests.Cpu

open NUnit.Framework
open Cpu8086

[<TestFixture>]
type Cpu8086Tests () =

    [<SetUp>]
    member this.SetUp() =
        resetCpu ()

    [<Test>]
    member this.CpuInitialStateCheck() =
        let registers = [AX; BX; CX; DX;]
        let clearedSegments = [DS; SS; ES;]

        for reg in registers do
            Assert.That(readRegister reg Word, Is.EqualTo(0));

        for seg in clearedSegments do
            Assert.That(readSegment seg, Is.EqualTo(0));

        Assert.That(readSegment CS, Is.EqualTo(0xFFFFus))
        Assert.That(readFlags (), Is.EqualTo(0xF002us))

    [<Test>]
    member this.CanReadAndWriteRegisters() =
        let registers = [AX; BX; CX; DX;]

        let testValue = 0xAAFFus
        let testValueLow = 0xFFus
        let testValueHigh = 0xAAus

        for reg in registers do
            writeRegister reg Word testValue
            Assert.That(readRegister reg Word, Is.EqualTo(testValue))
            Assert.That(readRegister reg High, Is.EqualTo(testValueHigh))
            Assert.That(readRegister reg Low, Is.EqualTo(testValueLow))

    [<Test>]
    member this.CanReadAndWriteSegments() =
        let segments = [CS; DS; SS; ES;]
        let testValue = 0xAAFFus

        for seg in segments do
            writeSegment seg testValue
            Assert.That(readSegment seg, Is.EqualTo(testValue));

    [<Test>]
    member this.CanReadAndWriteInstructionPointer() =
       writeIp 0xABCDus
       Assert.That(readIp (), Is.EqualTo(0xABCDus))

    [<Test>]
    member this.CanReadAndWriteFlagRegister() =
        writeFlags 0xABCDus
        Assert.That(readFlags (), Is.EqualTo(0xABCDus))

    [<Test>]
    member this.CanReadAndWriteFlags() =
        let initialValue = 0xF002us

        writeFlag CpuFlag.Carry
        Assert.That(readFlags (), Is.EqualTo(0x0001us ||| initialValue))

        writeFlag CpuFlag.Adjust
        Assert.That(readFlags (), Is.EqualTo(0x0011us ||| initialValue))

        writeFlag CpuFlag.Trap
        Assert.That(readFlags (), Is.EqualTo(0x0111us ||| initialValue))

        writeFlag CpuFlag.Interrupt
        Assert.That(readFlags (), Is.EqualTo(0x0311us ||| initialValue))

        writeFlag CpuFlag.Overflow
        Assert.That(readFlags (), Is.EqualTo(0x0B11us ||| initialValue))