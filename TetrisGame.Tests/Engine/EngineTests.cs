using Moq;
using Xunit;

namespace Tetris
{
   public class EngineTests
   {
       private KeyReceiver _keyReceiver;
       private Engine _engine;
       private Mock<IScreen> _screenMock;

       int _FPS = 30;

       public EngineTests()
       {
            _screenMock = new Mock<IScreen>();

            _keyReceiver = new KeyReceiver();

            _engine = new Engine(_FPS, _screenMock.Object, _keyReceiver);
       }

        [Fact]
        public void Constructor_MountsPassedScreenWithEngine()
        {
            _screenMock.Verify(screen => screen.Mount(_engine), Times.Once());
        }

        [Fact]
        public void Start_KeyReceiverStartListening()
        {
            _engine.Start();

            Assert.True(_keyReceiver.Listening);
        }

        [Fact]
        public void Loop_CallsScreenRender()
        {
            _engine.Loop();
            _screenMock.Verify(screen => screen.Render(), Times.Once());
        }

        [Fact]
        public void Loop_NoReceivedKey_CallsScreenInputWithNull()
        {
            _engine.Loop();
            int dTime = 1000 / _FPS;
            _screenMock.Verify(screen => screen.Input(null, dTime), Times.Once());
        }

        [Fact]
        public void SwitchScreen_UnmountsCurrentScreenWithEngine()
        {
            Mock<IScreen> _screenMock2 = new Mock<IScreen>();
            _engine.SwitchScreen(_screenMock2.Object);
            _screenMock.Verify(screen => screen.Unmount(_engine), Times.Once());
        }

        [Fact]
        public void SwitchScreen_MountsNewScreenWithEngine()
        {
            Mock<IScreen> _screenMock2 = new Mock<IScreen>();
            _engine.SwitchScreen(_screenMock2.Object);
            _screenMock2.Verify(screen => screen.Mount(_engine), Times.Once());
        }

        [Fact]
        public void Stop_KeyReceiverStopsListening()
        {
            _engine.Start();
            _engine.Stop();

            Assert.False(_keyReceiver.Listening);
        }
   }
}