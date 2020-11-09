using GDGeek;
using Mirror;
using MrPP.Myth;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace MrPP.Game { 
    public class Robot : NetworkBehaviour, IBridgeReceiver
	{
        Animator _animator;
        // Start is called before the first frame update
        void Start()
        {
            _animator = gameObject.GetComponent<Animator>();
			BridgeBroadcast.Instance?.addReceiver(this);
        }
		void OnDestory() {

			BridgeBroadcast.Instance?.removeReceiver(this);
		}
		void Update()
		{   
			//CheckKey();
		}
		[SerializeField]
		private bool _walk_anim = false;
		[SerializeField]
		private bool _roll_anim = false;
		[SerializeField]
		private bool _open_anim = false;

		public string handle => this.longName();

		public override void OnStartServer() {
			
		}
		void CheckKey()
		{
			
			if (_animator.GetBool("Walk_Anim") != _walk_anim) {
				_animator.SetBool("Walk_Anim", _walk_anim);
			}
			if (_animator.GetBool("Roll_Anim") != _roll_anim)
			{
				_animator.SetBool("Roll_Anim", _roll_anim);
			}
			if (_animator.GetBool("Open_Anim") != _open_anim)
			{
				_animator.SetBool("Open_Anim", _open_anim);
			}
		}
		public void change() {

			Bridge.Instance?.post(handle, "change");
		}

		public void broadcast(string evt, object data)
		{
			if(evt == "change") {
				_animator.SetBool("Open_Anim",!(bool)_animator.GetBool("Open_Anim"));
			}
		}
	}
}