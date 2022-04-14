
namespace Simpu
{
    public enum Instructions : byte
    {
        NOP = 0x00,
        
        JUMP_ABSOLUTE = 0x01,
        JUMP_RELATIVE_BACK = 0x02,

        MOVE_ADDRESS_VALUE = 0x10,
        MOVE_ADDRESS_REG = 0x11,
        MOVE_REG_ADDRESS = 0x12,

        ARITHMETIC_ADD = 0x20
    }
}
