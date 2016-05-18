using UnityEngine.UI;
using System.Collections.Generic;
using DotOriko;
using LinqTools;
using UnityEngine;

namespace DotOriko.UI.Animations {
	public class ImageSequenceAnimator : DotOrikoUI {

		[SerializeField]
		private Image _image;

		[SerializeField]
		private List<Sprite> _sprites;

		[Range(0, 60)]
		[SerializeField]
		private int FPS = 10;

		protected override void OnStart () {
			base.OnStart ();

			_sprites = _sprites.OrderBy (s => s.name) as List<Sprite>;
		}

		protected override void OnUpdate () {
			base.OnUpdate ();

			int index = (int)(Time.time * FPS) % _sprites.Count;
			_image.sprite = _sprites [index];
		}
	}
}
