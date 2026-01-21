public abstract class State 
{  protected StateMachine context;
   protected bool isBaseState = false;
   protected State currentSubState;
   protected State currentSuperState;
   public State(StateMachine currentContext)
   {
      context = currentContext;
   }
   public abstract void EnterState();
   public abstract void UpdateState();
   public abstract void ExitState();
   public abstract void CheckSwitchStates();
   public virtual void InitializeSubStates(){}
   public void UpdateStates()
   {
      UpdateState();
      if (currentSubState != null)
      {
         currentSubState.UpdateStates();
      }
   }

   public void EnterStates()
   {
      EnterState();
      if (currentSubState != null)
      {
         currentSubState.EnterStates();
      }
   }
   public void SwitchState(State newState)
   {
      ExitState();
      newState.EnterStates();
      if (isBaseState)
      {
         context.CurrentState = newState;
      } else
      {
         currentSuperState.SetSubState(newState);
      }
      
   }

   protected void SetSuperState(State newSuperState)
   {
      currentSuperState = newSuperState;
   }
   protected void SetSubState(State newSubState)
   {
      currentSubState = newSubState;
      currentSubState.SetSuperState(this);
   }

}
