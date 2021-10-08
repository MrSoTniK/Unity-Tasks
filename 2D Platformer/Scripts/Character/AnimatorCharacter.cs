using System.Collections;
using System.Collections.Generic;

public static class AnimatorCharacter
{
    public static class Params 
    {
        public const string Speed = nameof(Speed);
        public const string IsJumping = nameof(IsJumping);
        public const string IsAttack = nameof(IsAttack);
    }

    public static class States 
    {
        public const string CharacterIdle = nameof(CharacterIdle);
        public const string CharacterRun = nameof(CharacterRun);
        public const string CharacterJump = nameof(CharacterJump);
        public const string CharacterAttack = nameof(CharacterAttack);
    }
}
