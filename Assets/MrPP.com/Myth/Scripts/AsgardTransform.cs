using GDGeek;
using UnityEngine;
using Mirror;
namespace MrPP.Myth
{

    public class AsgardTransform : NetworkBehaviour
    {

        [SerializeField]
        private Transform _transform = null;
        public Transform getTransform() {
            if (_transform == null) {
                _transform = transform;
            }
            return _transform;
        }
        [SerializeField]
        private bool _interpolation = true;

        [SyncVar(hook = "dataChange")]
        public Yggdrasil.AsgardPose _pose;

        private void dataChange(Yggdrasil.AsgardPose oldValue, Yggdrasil.AsgardPose newValue) {
           
            if (!this.hasAuthority) {
                Yggdrasil.WorldPose world = Yggdrasil.Instance.getWorldPose(newValue); 
                if (_interpolation && Vector3.Distance(world.position, getTransform().position) < 0.3f)
                {
                    TweenTransformData.Begin(getTransform().gameObject, 0.1f, world.position, Quaternion.LookRotation(world.forward, world.up), world.scale);
                }
                else
                {
                    getTransform().position = world.position;
                    getTransform().rotation = Quaternion.LookRotation(world.forward, world.up);
                    getTransform().setGlobalScale(world.scale);
                }
            }
        }
        [Command]
        public void CmdSetPose(Yggdrasil.AsgardPose pose)
        {
            _pose = pose;
        }
        IsChanged chaned = new IsChanged();
        private void Update()
        {
           // Debug.LogError(this.hasAuthority);
            if (Yggdrasil.IsInitialized && this.hasAuthority && isClient && chaned.hasChanged(getTransform())) {

                CmdSetPose(Yggdrasil.Instance.getAsgardPose(new Yggdrasil.WorldPose(getTransform())));
            }  
        }
     
        [Command]
        private void CmdDestroy() {
            NetworkServer.Destroy(this.gameObject);

        }
        private void OnDestroy()
        {
            if(this.hasAuthority){
                if(this.isClient){
                    CmdDestroy();
                }else if(this.isServer){
                    NetworkServer.Destroy(this.gameObject);
                }
            }
        }
    }
}
