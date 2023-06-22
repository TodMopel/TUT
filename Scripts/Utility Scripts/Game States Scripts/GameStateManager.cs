namespace TodMopel {
    public class GameStateManager
    {
        private static GameStateManager gameStateInstance;

        public static GameStateManager Instance {
            get {
                if (gameStateInstance == null)
                    gameStateInstance = new GameStateManager();

                return gameStateInstance;
            }
		}
        public GameState CurrentGameState { get; private set; }

        public delegate void GameStateChangeHandler(GameState newGameState);
        public event GameStateChangeHandler OnGameStateChanged;

        public void SetState(GameState newGameState)
        {
            if (newGameState == CurrentGameState)
                return;
            CurrentGameState = newGameState;
            OnGameStateChanged?.Invoke(newGameState);
        }

        private GameStateManager()
		{

		}
    }
}