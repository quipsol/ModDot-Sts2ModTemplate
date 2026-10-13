using BaseLib.Abstracts;
using BaseLib.Extensions;
using BaseLib.Utils;
using CharMod.Character;
using CharMod.Core.Extensions;

namespace CharMod.Core.Models;

[Pool(typeof(CharModPotionPool))]
public abstract class CharModPotionModel : CustomPotionModel
{
	public override string? CustomPackedImagePath =>
		$"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".PotionImagePath();
	public override string? CustomPackedOutlinePath =>
		$"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".PotionOutlineImagePath();
}