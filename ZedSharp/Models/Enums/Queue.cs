﻿// ReSharper disable InconsistentNaming
namespace ZedSharp.Models.Enums
{
    public enum Queue
    {
        CUSTOM = 0,
        NORMAL_3x3 = 8,
        NORMAL_5x5_BLIND = 2,
        NORMAL_5x5_DRAFT = 14,
        RANKED_SOLO_5x5 = 4,
        RANKED_PREMADE_5x5 = 6, // DEPRECATED
        RANKED_FLEX_TT = 9, // Used for both historical RANKED_PREMADE_3x3* and current 
        RANKED_TEAM_3x3 = 41, // DEPRECATED
        RANKED_TEAM_5x5 = 42,
        ODIN_5x5_BLIND = 16,
        ODIN_5x5_DRAFT = 17,
        BOT_5x5 = 7, // DEPRECATED
        BOT_ODIN_5x5 = 25,
        BOT_5x5_INTRO = 31,
        BOT_5x5_BEGINNER = 32,
        BOT_5x5_INTERMEDIATE = 33,
        BOT_TT_3x3 = 52,
        GROUP_FINDER_5x5 = 61,
        ARAM_5x5 = 65,
        ONEFORALL_5x5 = 70,
        FIRSTBLOOD_1x1 = 72,
        FIRSTBLOOD_2x2 = 73,
        SR_6x6 = 75,
        URF_5x5 = 76,
        ONEFORALL_MIRRORMODE_5x5 = 78,
        BOT_URF_5x5 = 83,
        NIGHTMARE_BOT_5x5_RANK1 = 91,
        NIGHTMARE_BOT_5x5_RANK2 = 92,
        NIGHTMARE_BOT_5x5_RANK5 = 93,
        ASCENSION_5x5 = 96,
        HEXAKILL = 98,
        BILGEWATER_ARAM_5x5 = 100,
        KING_PORO_5x5 = 300,
        COUNTER_PICK = 310,
        BILGEWATER_5x5 = 313,
        SIEGE = 315,
        DEFINITELY_NOT_DOMINION_5x5 = 317,
        ARURF_5X5 = 318,
        ARSR_5x5 = 325,
        TEAM_BUILDER_DRAFT_UNRANKED_5x5 = 400,
        TEAM_BUILDER_DRAFT_RANKED_5x5 = 410, // DEPRECATED
        TEAM_BUILDER_RANKED_SOLO = 420,
        TB_BLIND_SUMMONERS_RIFT_5x5 = 430,
        RANKED_FLEX_SR = 440,
        ASSASSINATE_5x5 = 600,
        DARKSTAR_3x3 = 610
    }
}