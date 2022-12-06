using System;
using RangerRPG.Core;
using UnityEngine;
using UnityEngine.Events;

namespace MiniKnight.Area {
    public class AreaTask : MonoBehaviour {

        public string taskDescription = "Hidden";
        private UnityEvent<AreaTask> onComplete = new();
        private bool isCompleted = false;
        
        public void Completed() {
            isCompleted = true; 
            Log.Info($"Task Completed! {taskDescription}");
            onComplete.Invoke(this);
        }
        
        public void AddListener(UnityAction<AreaTask> taskCompleted) {
            onComplete.AddListener(taskCompleted);
        }

        public bool IsCompleted() {
            return isCompleted;
        }
    }
}