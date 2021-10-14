using System.Collections;
using MonoBehaviours;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Tests.PlayMode
{
    public class AcceptanceTests
    {
        [UnityTest]
        public IEnumerator GameAutomaticallyStarts()
        {
            var gameStarted = false;
            Game.GameStarted += () => gameStarted = true;

            Object.Instantiate(Resources.Load<GameObject>("Prefabs/Game"));

            yield return null;

            Assert.IsTrue(gameStarted);
        }

        [UnityTest]
        public IEnumerator PlayerControlsABird()
        {
            GameObject playerGameObject = null;
            Game.PlayerSpawned += playerSpawned => playerGameObject = playerSpawned;

            Object.Instantiate(Resources.Load<GameObject>("Prefabs/Game"));
            yield return null;

            Assert.NotNull(playerGameObject);
        }
    }
}