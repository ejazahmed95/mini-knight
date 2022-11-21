using System;
using UnityEngine;
using UnityEngine.Events;

namespace MiniKnight.Area {
    public class AreaTask : MonoBehaviour {

        public string taskDescription = "Hidden";
        private UnityEvent<AreaTask> onComplete = new();
        private bool isCompleted = false;
        
        public void Completed() {
            isCompleted = true; 
            onComplete.Invoke(this);
        }
        
        public void AddListener(Action<AreaTask> taskCompleted) {
            taskCompleted.Invoke(this);
        }

        public bool IsCompleted() {
            return isCompleted;
        }
    }
}