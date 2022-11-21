using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace MiniKnight.Area {
    public class AreaManager : MonoBehaviour {
        public List<AreaTask> tasks;
        public UnityEvent<AreaManager> areaClearedEvent = new();

        private int _tasksCompleted = 0;
        
        private void Awake() {
            foreach (var task in tasks) {
                task.AddListener(TaskCompleted);
            }
        }

        public void TaskCompleted(AreaTask task) {
            CheckAllTasksCompleted();
        }
        
        private bool CheckAllTasksCompleted() {
            foreach (var task in tasks) {
                if (task.IsCompleted() == false) {
                    return false;
                }
            }
            return true;
        }
    }
}