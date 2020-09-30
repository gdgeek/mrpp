
using UnityEngine;


namespace MrPP.Input{

	[RequireComponent(typeof(MrPP.Input.Clicker)), RequireComponent(typeof(Inputable))]
	public class ButtonFocusable : MonoBehaviour, IInputHandler
    {

        public void focusEnter() {
            start();
        }
        public void focusExit()
        {
            over();
        }
        public void inputDown()
        {
            over();
        }
        public void inputUp()
        {
            over();
        }

        private bool running_ = false;
		private float time_ = 0.0f;
		[SerializeField]
		private float _foucusTime = 2.0f;
		private void start(){
			running_ = true;
			time_ = 0.0f;
            if (countown_ != null) {
                countown_.open();
            }

        }
		private void over(){
			running_ = false;
            time_ = 0.0f;


            if (countown_ != null)
            {
                countown_.close();
            }
        }

      
        private void execute(){
            IInputHandler[] effects = this.gameObject.GetComponents<IInputHandler>();
            foreach (var e in effects)
            {
                e.inputDown();
            }

            if (clicker_ && clicker_.enabled)
            {
                if (clicker_.onClicked != null)
                {
                    clicker_.onClicked.Invoke();
                }
            }


		}
			
		private MrPP.Input.Clicker clicker_ = null;
        private ICountdown countown_ = null; 
        // private MrPP.UX.InputHandler input_ = null;
        // Use this for initialization
        void Start ()
        {

            countown_ = this.gameObject.GetComponentInChildren<ICountdown>();
            clicker_ = this.gameObject.GetComponent<Clicker>();
            running_ = false;
		}
			
		// Update is called once per frame
		void Update () {
			if (this.running_) {
				time_ += Time.deltaTime;

                if (time_ > this._foucusTime)
                {


                    if (countown_ != null)
                    {
                        countown_.percent(1f);
                    }
                    this.over();
                    this.execute();
                }
                else {
                    if (countown_ != null)
                    {
                        countown_.percent(time_ / this._foucusTime);
                    }
                }
			}
		}

        public void close()
        {
        }

        public void open()
        {
        }
    }
}