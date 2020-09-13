using Moq;
using Xunit;

namespace Tetris
{
    public class EngineTests
    {
        private readonly KeyReceiver _keyReceiver;
        private readonly Engine _engine;
        private readonly Mock<IScreen> _screenMock;
        private readonly int _fps = 30;

        public EngineTests()
        {
            _screenMock = new Mock<IScreen>();

            _keyReceiver = new KeyReceiver();

            _engine = new Engine(_fps, _screenMock.Object, _keyReceiver);
        }

        [Fact]
        public void Constructor_MountScreenWithEngine()
        {
            _screenMock.Verify(screen => screen.Mount(_engine), Times.Once());
        }

        [Fact]
        public void Start_StartedTrue()
        {
            _engine.Start();

            Assert.True(_engine.Started);
        }

        [Fact]
        public void Loop_CallScreenRender()
        {
            _engine.Loop();
            _screenMock.Verify(screen => screen.Render(), Times.Once());
        }

        [Fact]
        public void Loop_NoReceivedKey_CallScreenInputWithNull()
        {
            _engine.Loop();
            int dTime = 1000 / _fps;
            _screenMock.Verify(screen => screen.Input(null, dTime), Times.Once());
        }

        [Fact]
        public void Loop_ReceivedKey_CallScreenInputWithKey()
        {
            _keyReceiver.ReceiveKey("Any key");
            _engine.Loop();
            int dTime = 1000 / _fps;
            _screenMock.Verify(screen => screen.Input("Any key", dTime), Times.Once());
        }

        [Fact]
        public void SwitchScreen_UnmountCurrentScreenWithEngine()
        {
            Mock<IScreen> screenMock2 = new Mock<IScreen>();
            _engine.SwitchScreen(screenMock2.Object);
            _screenMock.Verify(screen => screen.Unmount(_engine), Times.Once());
        }

        [Fact]
        public void SwitchScreen_MountNewScreenWithEngine()
        {
            Mock<IScreen> screenMock2 = new Mock<IScreen>();
            _engine.SwitchScreen(screenMock2.Object);
            screenMock2.Verify(screen => screen.Mount(_engine), Times.Once());
        }

        [Fact]
        public void Stop_StartedFalse()
        {
            _engine.Start();
            _engine.Stop();

            Assert.False(_engine.Started);
        }
    }
}
