using System;

namespace SHARMemory.SHAR;

public partial class Globals
{
#pragma warning disable IDE1006 // Naming Styles
    public static class pddiEnums
    {
        public enum pddiDisplayMode
        {
            PDDI_DISPLAY_FULLSCREEN,
            PDDI_DISPLAY_WINDOW,
            PDDI_DISPLAY_FULLSCREEN_PAL
        };

        public enum pddiLockType
        {
            PDDI_LOCK_READONLY,
            PDDI_LOCK_WRITEONLY,
            PDDI_LOCK_READWRITE
        };

        public enum pddiMatrixType
        {
            PDDI_MATRIX_MODELVIEW,
            PDDI_MATRIX_TEXTURE0,
            PDDI_MATRIX_TEXTURE1,
            PDDI_MATRIX_TEXTURE2,
            PDDI_MATRIX_TEXTURE3
        };

        public enum pddiProjectionMode
        {
            PDDI_PROJECTION_PERSPECTIVE,
            PDDI_PROJECTION_ORTHOGRAPHIC,
            PDDI_PROJECTION_DEVICE
        };

        [Flags]
        public enum pddiBufferMask : uint
        {
            PDDI_BUFFER_COLOUR = 1,
            PDDI_BUFFER_DEPTH = 2,
            PDDI_BUFFER_STENCIL = 4,
            PDDI_BUFFER_ALL = 0xffffffff
        };

        public enum pddiCompareMode
        {
            PDDI_COMPARE_NONE,
            PDDI_COMPARE_ALWAYS,
            PDDI_COMPARE_LESS,
            PDDI_COMPARE_LESSEQUAL,
            PDDI_COMPARE_GREATER,
            PDDI_COMPARE_GREATEREQUAL,
            PDDI_COMPARE_EQUAL,
            PDDI_COMPARE_NOTEQUAL
        };

        public enum pddiStencilOp
        {
            PDDI_STENCIL_KEEP,
            PDDI_STENCIL_ZERO,
            PDDI_STENCIL_REPLACE,
            PDDI_STENCIL_INCR,
            PDDI_STENCIL_DECR,
            PDDI_STENCIL_INVERT
        };

        public enum pddiFillMode
        {
            PDDI_FILL_SOLID,
            PDDI_FILL_WIRE,
            PDDI_FILL_POINT
        };

        public enum pddiCullMode
        {
            PDDI_CULL_NONE,
            PDDI_CULL_NORMAL,
            PDDI_CULL_INVERTED,
            PDDI_CULL_SHADOW_BACKFACE,
            PDDI_CULL_SHADOW_FRONTFACE
        };

        public enum pddiTextureBlendMode
        {
            PDDI_TEXBLEND_DECAL,
            PDDI_TEXBLEND_DECALALPHA,
            PDDI_TEXBLEND_MODULATE,
            PDDI_TEXBLEND_MODULATEALPHA,
            PDDI_TEXBLEND_ADD,
            PDDI_TEXBLEND_MODULATEINTENSITY
        };

        public enum pddiUVMode
        {
            PDDI_UV_TILE,
            PDDI_UV_CLAMP
        };

        public enum pddiFilterMode
        {
            PDDI_FILTER_NONE,
            PDDI_FILTER_BILINEAR,
            PDDI_FILTER_MIPMAP,
            PDDI_FILTER_MIPMAP_BILINEAR,
            PDDI_FILTER_MIPMAP_TRILINEAR
        };

        public enum pddiLightType
        {
            PDDI_LIGHT_DIRECTIONAL,
            PDDI_LIGHT_POINT,
            PDDI_LIGHT_SPOT
        };

        public enum pddiVertexMask
        {
            PDDI_V_UVCOUNT0 = 0,
            PDDI_V_UVCOUNT1 = 1,
            PDDI_V_UVCOUNT2 = 2,
            PDDI_V_UVCOUNT3 = 3,
            PDDI_V_UVCOUNT4 = 4,
            PDDI_V_UVCOUNT5 = 5,
            PDDI_V_UVCOUNT6 = 6,
            PDDI_V_UVCOUNT7 = 7,
            PDDI_V_UVCOUNT8 = 8,
            PDDI_V_UVMASK = 15,

            PDDI_V_NORMAL = 1 << 4,
            PDDI_V_COLOUR = 1 << 5,
            PDDI_V_SPECULAR = 1 << 6,
            PDDI_V_INDICES = 1 << 7,
            PDDI_V_WEIGHTS = 1 << 8,
            PDDI_V_SIZE = 1 << 9,
            PDDI_V_W = 1 << 10,
            PDDI_V_BINORMAL = 1 << 11,
            PDDI_V_TANGENT = 1 << 12,
            PDDI_V_POSITION = 1 << 13,
            PDDI_V_COLOUR2 = 1 << 14,

            PDDI_V_COLOUR_COUNT0 = 0 << 15,
            PDDI_V_COLOUR_COUNT1 = 1 << 15,
            PDDI_V_COLOUR_COUNT2 = 2 << 15,
            PDDI_V_COLOUR_COUNT3 = 3 << 15,
            PDDI_V_COLOUR_COUNT4 = 4 << 15,
            PDDI_V_COLOUR_COUNT5 = 5 << 15,
            PDDI_V_COLOUR_COUNT6 = 6 << 15,
            PDDI_V_COLOUR_COUNT7 = 7 << 15,
            PDDI_V_COLOUR_MASK = 7 << 15,
            PDDI_V_COLOUR_MASK_OFFSET = 15
        };

        public enum pddiPrimType
        {
            PDDI_PRIM_TRIANGLES,
            PDDI_PRIM_TRISTRIP,
            PDDI_PRIM_LINES,
            PDDI_PRIM_LINESTRIP,
            PDDI_PRIM_POINTS
        };

        [Flags]
        public enum pddiStateMask : uint
        {
            PDDI_STATE_RENDER = (1 << 0),
            PDDI_STATE_VIEW = (1 << 1),
            PDDI_STATE_LIGHTING = (1 << 2),
            PDDI_STATE_FOG = (1 << 3),
            PDDI_STATE_STENCIL = (1 << 4),
            PDDI_STATE_ALL = (0xffffffff)
        };

        public enum pddiStatType
        {
            PDDI_STAT_CURRENT_FRAME,
            PDDI_STAT_FRAME_TIME,
            PDDI_STAT_BUFFERED_PRIM,
            PDDI_STAT_BUFFERED_PRIM_VERT,
            PDDI_STAT_BUFFERED_INDEXED_PRIM,
            PDDI_STAT_BUFFERED_INDEXED_PRIM_VERT,
            PDDI_STAT_BUFFERED_PRIM_CALLS,
            PDDI_STAT_BUFFERED_PRIM_AVG,
            PDDI_STAT_BUFFERED_COUNT,
            PDDI_STAT_BUFFERED_ALLOC,
            PDDI_STAT_STREAMED_PRIM,
            PDDI_STAT_STREAMED_PRIM_VERT,
            PDDI_STAT_STREAMED_PRIM_CALLS,
            PDDI_STAT_STREAMED_PRIM_AVG,
            PDDI_STAT_SKINNED_BONES,
            PDDI_STAT_SKINNED_XFORM_VERT,
            PDDI_STAT_SKINNED_XFORM_MS,
            PDDI_STAT_SKINNED_WAIT_MS,
            PDDI_STAT_MATRIX_OPS,
            PDDI_STAT_LIGHT_OPS,
            PDDI_STAT_MATERIAL_OPS,
            PDDI_STAT_TEXTURE_HITS,
            PDDI_STAT_TEXTURE_MISSES,
            PDDI_STAT_TEXTURE_UPLOADED,
            PDDI_STAT_TEXTURE_ALLOCATED,
            PDDI_STAT_TEXTURE_SLOP,
            PDDI_STAT_TEXTURE_COUNT_4BIT,
            PDDI_STAT_TEXTURE_ALLOC_4BIT,
            PDDI_STAT_TEXTURE_COUNT_8BIT,
            PDDI_STAT_TEXTURE_ALLOC_8BIT,
            PDDI_STAT_TEXTURE_COUNT_16BIT,
            PDDI_STAT_TEXTURE_ALLOC_16BIT,
            PDDI_STAT_TEXTURE_COUNT_32BIT,
            PDDI_STAT_TEXTURE_ALLOC_32BIT,
            PDDI_STAT_TEXTURE_COUNT_DXTN,
            PDDI_STAT_TEXTURE_ALLOC_DXTN
        };

        public enum pddiTextureOrigin
        {
            PDDI_ORIGIN_TOP,
            PDDI_ORIGIN_BOTTOM
        };

        public enum pddiTextureType
        {
            PDDI_TEXTYPE_RGB,
            PDDI_TEXTYPE_PALETTIZED,
            PDDI_TEXTYPE_LUMINANCE,
            PDDI_TEXTYPE_BUMPMAP,
            PDDI_TEXTYPE_DXT1,
            PDDI_TEXTYPE_DXT2,
            PDDI_TEXTYPE_DXT3,
            PDDI_TEXTYPE_DXT4,
            PDDI_TEXTYPE_DXT5,
            PDDI_TEXTYPE_IPU,
            PDDI_TEXTYPE_Z,
            PDDI_TEXTYPE_LINEAR,
            PDDI_TEXTYPE_RENDERTARGET,
            PDDI_TEXTYPE_PS2_4BIT,
            PDDI_TEXTYPE_PS2_8BIT,
            PDDI_TEXTYPE_PS2_16BIT,
            PDDI_TEXTYPE_PS2_32BIT,
            PDDI_TEXTYPE_GC_4BIT,
            PDDI_TEXTYPE_GC_8BIT,
            PDDI_TEXTYPE_GC_16BIT,
            PDDI_TEXTYPE_GC_32BIT,
            PDDI_TEXTYPE_GC_DXT1
        };

        public enum pddiPixelFormat
        {
            PDDI_PIXEL_UNKNOWN,
            PDDI_PIXEL_RGB565,
            PDDI_PIXEL_ARGB1555,
            PDDI_PIXEL_RGB555,
            PDDI_PIXEL_ARGB4444,
            PDDI_PIXEL_RGB888,
            PDDI_PIXEL_ARGB8888,
            PDDI_PIXEL_PAL8,
            PDDI_PIXEL_PAL4,
            PDDI_PIXEL_LUM8,
            PDDI_PIXEL_DUDV88,
            PDDI_PIXEL_DXT1,
            PDDI_PIXEL_DXT2,
            PDDI_PIXEL_DXT3,
            PDDI_PIXEL_DXT4,
            PDDI_PIXEL_DXT5,
            PDDI_PIXEL_Z32,
            PDDI_PIXEL_Z24,
            PDDI_PIXEL_Z16,
            PDDI_PIXEL_Z8,
            PDDI_PIXEL_PS2_4BIT,
            PDDI_PIXEL_PS2_8BIT,
            PDDI_PIXEL_PS2_16BIT,
            PDDI_PIXEL_PS2_32BIT,
            PDDI_PIXEL_GC_4BIT,
            PDDI_PIXEL_GC_8BIT,
            PDDI_PIXEL_GC_16BIT,
            PDDI_PIXEL_GC_32BIT,
            PDDI_PIXEL_GC_DXT1
        };

        public enum pddiTextureUsageHint
        {
            PDDI_USAGE_STATIC,
            PDDI_USAGE_DYNAMIC,
            PDDI_USAGE_NOCACHE
        };

        public enum pddiBlendMode
        {
            PDDI_BLEND_NONE,
            PDDI_BLEND_ALPHA,
            PDDI_BLEND_ADD,
            PDDI_BLEND_SUBTRACT,
            PDDI_BLEND_MODULATE,
            PDDI_BLEND_MODULATE2,
            PDDI_BLEND_ADDMODULATEALPHA,
            PDDI_BLEND_SUBMODULATEALPHA,
            PDDI_BLEND_DESTALPHA
        };

        public enum pddiShadeMode
        {
            PDDI_SHADE_FLAT,
            PDDI_SHADE_GOURAUD
        };

        public enum pddiTextureGen
        {
            PDDI_TEXGEN_NONE,
            PDDI_TEXGEN_ENVMAP,
            PDDI_TEXGEN_SPHEREMAP
        };

        public enum pddiMultiCBVBlendMode
        {
            PDDI_MULTI_CBV_BLEND_NONE,
            PDDI_MULTI_CBV_BLEND_ADD,
            PDDI_MULTI_CBV_BLEND_SUBTRACT,
            PDDI_MULTI_CBV_BLEND_MODULATE,
            PDDI_MULTI_CBV_BLEND_INTERPOLATE
        };

        public enum pddiMultiCBVBlendOperand
        {
            PDDI_MULTI_CBV_BLEND_COLOUR,
            PDDI_MULTI_CBV_SET1,
            PDDI_MULTI_CBV_SET2,
            PDDI_MULTI_CBV_SET3,
            PDDI_MULTI_CBV_SET4,
            PDDI_MULTI_CBV_SET5,
            PDDI_MULTI_CBV_SET6,
            PDDI_MULTI_CBV_SET7
        };

        public enum pddiBlendFactor
        {
            PDDI_BF_ZERO,
            PDDI_BF_ONE,
            PDDI_BF_SRC,
            PDDI_BF_ONE_MINUS_SRC,
            PDDI_BF_DEST,
            PDDI_BF_ONE_MINUS_DEST,
            PDDI_BF_SRCALPHA,
            PDDI_BF_ONE_MINUS_SRCALPHA,
            PDDI_BF_DESTALPHA,
            PDDI_BF_ONE_MINUS_DESTALPHA,
            PDDI_BF_SRCALPHASATURATE
        };
    }
#pragma warning restore IDE1006 // Naming Styles
}
