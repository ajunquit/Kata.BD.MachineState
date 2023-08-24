using System.Linq;

namespace Kata.BD.MachineState.Test
{
    public class UnitTestMachineState
    {
        [Theory]
        [InlineData("ESTABLISHED", "APP_PASSIVE_OPEN APP_SEND RCV_SYN_ACK")]
        [InlineData("SYN_SENT", "APP_ACTIVE_OPEN")]
        [InlineData("FIN_WAIT_1", "APP_PASSIVE_OPEN RCV_SYN RCV_ACK APP_CLOSE")]
        [InlineData("FIN_WAIT_1", "APP_ACTIVE_OPEN RCV_SYN_ACK APP_CLOSE")]
        [InlineData("CLOSING", "APP_PASSIVE_OPEN RCV_SYN RCV_ACK APP_CLOSE RCV_FIN")]
        [InlineData("ERROR", "APP_ACTIVE_OPEN RCV_SYN_ACK APP_CLOSE RCV_FIN_ACK RCV_ACK")]
        public void Test_ValidInput_ReturnsCorrectResult(string expected, string input)
        {
            /// Arrange
            string actual = Kata.MachineState(input);

            /// Assert
            Assert.NotNull(actual);
            Assert.NotEmpty(actual);
            Assert.True(actual.Count() > 0);
            Assert.Equal(expected, actual);
        }
    }
}