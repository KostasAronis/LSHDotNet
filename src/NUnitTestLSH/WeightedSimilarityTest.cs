﻿using LSHDotNet;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace NUnitTestLSH
{
    public class WeightedSimilarityTest
    {
        const int signatureSize = 200;
        MinHasher<int> minHasher;
        private LSHSearch<int> lshSearcher;
        int[][] hashSeeds = new int[][]
        {
            new int[]{ 393494444, 1835015591 },
            new int[]{ 1542017581, 1705388083 },
            new int[]{ 1632061126, 464881423 },
            new int[]{ 199594887, 851946104 },
            new int[]{ 735117055, 73537587 },
            new int[]{ 2014322928, 198312065 },
            new int[]{ 1437570080, 783385061 },
            new int[]{ 1513868874, 443741342 },
            new int[]{ 527756253, 1190835857 },
            new int[]{ 1111117181, 195751221 },
            new int[]{ 2069635344, 1379118942 },
            new int[]{ 1449933272, 1810931495 },
            new int[]{ 1357586260, 1548663501 },
            new int[]{ 798449616, 2099056181 },
            new int[]{ 1067836040, 364861328 },
            new int[]{ 2114650798, 1627848252 },
            new int[]{ 1337763552, 320806896 },
            new int[]{ 930744073, 323254383 },
            new int[]{ 743196921, 2063875240 },
            new int[]{ 398807240, 976908116 },
            new int[]{ 422781620, 771196724 },
            new int[]{ 102753187, 1837600924 },
            new int[]{ 779598353, 725592655 },
            new int[]{ 2080237784, 66994610 },
            new int[]{ 149153925, 1746762460 },
            new int[]{ 2080921342, 1752605403 },
            new int[]{ 391596053, 86377881 },
            new int[]{ 159772709, 1161859149 },
            new int[]{ 385082319, 1878569733 },
            new int[]{ 347801823, 83397625 },
            new int[]{ 1813915454, 248022353 },
            new int[]{ 1931593711, 370255727 },
            new int[]{ 106370436, 386474676 },
            new int[]{ 1008032160, 1116763184 },
            new int[]{ 2000124635, 1190614491 },
            new int[]{ 1848028068, 611364660 },
            new int[]{ 792028617, 134209065 },
            new int[]{ 1920453248, 1298438620 },
            new int[]{ 1276365755, 1759815995 },
            new int[]{ 1031333142, 631993605 },
            new int[]{ 1615909364, 731455006 },
            new int[]{ 1949902256, 1468557227 },
            new int[]{ 431423633, 362045395 },
            new int[]{ 1236252199, 1251385671 },
            new int[]{ 161034187, 1916368571 },
            new int[]{ 2085655711, 1012110835 },
            new int[]{ 1716073417, 315409615 },
            new int[]{ 1310476309, 174759267 },
            new int[]{ 987086660, 1879981107 },
            new int[]{ 1731230488, 393123677 },
            new int[]{ 1865044142, 963474600 },
            new int[]{ 214353622, 1106023081 },
            new int[]{ 2046218039, 1469556682 },
            new int[]{ 960576786, 257386988 },
            new int[]{ 313408280, 1008817736 },
            new int[]{ 2032977041, 772749971 },
            new int[]{ 847236591, 1863291865 },
            new int[]{ 614971908, 1082460448 },
            new int[]{ 445603744, 463036484 },
            new int[]{ 2086315741, 1891808688 },
            new int[]{ 1297706124, 1904130136 },
            new int[]{ 955728997, 83756064 },
            new int[]{ 1252442427, 835917233 },
            new int[]{ 1042774890, 476619002 },
            new int[]{ 971216403, 1745693981 },
            new int[]{ 311351960, 1543868295 },
            new int[]{ 28585507, 638209465 },
            new int[]{ 914433110, 652434764 },
            new int[]{ 517101384, 843879175 },
            new int[]{ 1569822835, 1109350598 },
            new int[]{ 1548952256, 978865211 },
            new int[]{ 937977391, 1299700098 },
            new int[]{ 2030875177, 1312905740 },
            new int[]{ 164874244, 2000265199 },
            new int[]{ 1847921354, 228015861 },
            new int[]{ 1876639170, 524050176 },
            new int[]{ 1941149013, 1986905447 },
            new int[]{ 1242901200, 2108397653 },
            new int[]{ 7745603, 130597558 },
            new int[]{ 2001064301, 1210300806 },
            new int[]{ 426781792, 483957784 },
            new int[]{ 1433654232, 715197946 },
            new int[]{ 697465776, 489108746 },
            new int[]{ 744164464, 209027126 },
            new int[]{ 948858755, 2110020791 },
            new int[]{ 565359064, 1749208216 },
            new int[]{ 1040697296, 976965143 },
            new int[]{ 342856432, 318840913 },
            new int[]{ 966152745, 1803512546 },
            new int[]{ 200364534, 2087020334 },
            new int[]{ 671042989, 1189993338 },
            new int[]{ 776181295, 743200542 },
            new int[]{ 2016538458, 1934785431 },
            new int[]{ 1750202929, 189163707 },
            new int[]{ 1542791912, 953519104 },
            new int[]{ 644689161, 386503826 },
            new int[]{ 990298521, 359522029 },
            new int[]{ 682568806, 1064994472 },
            new int[]{ 1692694626, 222779445 },
            new int[]{ 602234322, 1541766431 },
            new int[]{ 1210051529, 1144976031 },
            new int[]{ 601912451, 496402311 },
            new int[]{ 2130512942, 527341311 },
            new int[]{ 209055887, 65782985 },
            new int[]{ 160273816, 1260694793 },
            new int[]{ 2026598668, 91153748 },
            new int[]{ 1617218562, 1922526676 },
            new int[]{ 324925142, 404680489 },
            new int[]{ 1133104507, 785904371 },
            new int[]{ 835687005, 1228521070 },
            new int[]{ 1562890019, 114365741 },
            new int[]{ 1756027472, 391208677 },
            new int[]{ 108769251, 1685728343 },
            new int[]{ 751904598, 408871608 },
            new int[]{ 2104684851, 2083052919 },
            new int[]{ 1642010689, 652021493 },
            new int[]{ 99725261, 2070920291 },
            new int[]{ 1973178632, 183916381 },
            new int[]{ 765002175, 1961905976 },
            new int[]{ 1305706176, 44902878 },
            new int[]{ 1530180919, 1241616442 },
            new int[]{ 1608056642, 208135130 },
            new int[]{ 1657323022, 1166903363 },
            new int[]{ 585244198, 1316605004 },
            new int[]{ 1944623995, 675695030 },
            new int[]{ 1176450498, 1541627202 },
            new int[]{ 1923453835, 1843219783 },
            new int[]{ 1662467063, 1095685788 },
            new int[]{ 1536432206, 210703774 },
            new int[]{ 387633060, 444784599 },
            new int[]{ 1922920360, 1947667926 },
            new int[]{ 108581781, 224704544 },
            new int[]{ 1766167751, 1374577175 },
            new int[]{ 2138912134, 1693781918 },
            new int[]{ 2096831691, 141008761 },
            new int[]{ 1787161961, 1318682178 },
            new int[]{ 1627681842, 790784127 },
            new int[]{ 1845823798, 321273577 },
            new int[]{ 653792746, 1547892342 },
            new int[]{ 881369302, 1089349535 },
            new int[]{ 1100484145, 1582783241 },
            new int[]{ 611731260, 1428989821 },
            new int[]{ 906602421, 100383487 },
            new int[]{ 876051305, 403989125 },
            new int[]{ 408453228, 877492844 },
            new int[]{ 794967822, 554298401 },
            new int[]{ 1574272916, 860921577 },
            new int[]{ 269466165, 1729996640 },
            new int[]{ 1133034661, 1383352098 },
            new int[]{ 589451026, 282745847 },
            new int[]{ 1175474876, 1038945927 },
            new int[]{ 1367256960, 1803615234 },
            new int[]{ 1036016716, 2005251967 },
            new int[]{ 2061429007, 1132669708 },
            new int[]{ 2144879632, 1341193486 },
            new int[]{ 441893042, 2136023511 },
            new int[]{ 1476818119, 1445767172 },
            new int[]{ 1491784101, 340137119 },
            new int[]{ 1335936666, 827075607 },
            new int[]{ 1465585770, 1665784264 },
            new int[]{ 498525870, 1734923009 },
            new int[]{ 1285328690, 1219338847 },
            new int[]{ 1493524586, 1232863560 },
            new int[]{ 1891892909, 766760265 },
            new int[]{ 521317962, 115827158 },
            new int[]{ 1335722563, 1417924295 },
            new int[]{ 958441316, 598623455 },
            new int[]{ 2061358306, 61538218 },
            new int[]{ 215526281, 955599673 },
            new int[]{ 392973105, 1048834101 },
            new int[]{ 186438127, 1890865244 },
            new int[]{ 406593140, 1214743389 },
            new int[]{ 435599802, 806427958 },
            new int[]{ 1224963929, 128505744 },
            new int[]{ 1516621123, 2076812693 },
            new int[]{ 394059974, 305959054 },
            new int[]{ 2065249975, 1071150409 },
            new int[]{ 1931703624, 1588035514 },
            new int[]{ 1901100884, 147918113 },
            new int[]{ 310090648, 1950636803 },
            new int[]{ 113359058, 1294668742 },
            new int[]{ 611351746, 2029052474 },
            new int[]{ 5470923, 1171452394 },
            new int[]{ 1177582195, 878194664 },
            new int[]{ 1531892513, 1430245883 },
            new int[]{ 124610838, 380336993 },
            new int[]{ 434102502, 416751669 },
            new int[]{ 1479346137, 755144273 },
            new int[]{ 1328329869, 70585301 },
            new int[]{ 783739045, 687096628 },
            new int[]{ 7899631, 1763387165 },
            new int[]{ 1397622789, 591988916 },
            new int[]{ 1869250831, 1029763509 },
            new int[]{ 1500157967, 2034774554 },
            new int[]{ 814403478, 473322792 },
            new int[]{ 307920981, 67608168 },
            new int[]{ 645509025, 589819949 },
            new int[]{ 935475043, 1039253032 },
            new int[]{ 1279513498, 525024313 },
            new int[]{ 1209272466, 1411631055 },
        };
        Dictionary<int, ILSHWeightedHashable> productList = new Dictionary<int, ILSHWeightedHashable>()
        {
            {0, new Pr("C24F390FHU", "Samsung")},
            {1, new Pr("Dell", "S2719DGF")},
            {2, new Pr("Samsung", "CJG50")},
            {3, new Pr("AOC", "24G2U")},
            {4, new Pr("AOC", "C24G1")},
            {5, new Pr("Dell", "P2219H")},
            {6, new Pr("LG", "24TL510S-PZ")},
            {7, new Pr("AOC", "G2590VXQ")},
            {8, new Pr("AOC", "G2790PX")},
            {9, new Pr("Dell", "SE2719HR")},
            {10, new Pr("LG", "29WL500-B")},
            {11, new Pr("AOC", "CQ32G1")},
            {12, new Pr("AOC", "C27G1")},
            {13, new Pr("AOC", "C32G1")},
            {14, new Pr("Asus", "TUF Gaming VG27VQ")},
            {15, new Pr("LG", "27UL500-W")},
            {16, new Pr("Samsung", "LS24D330HSX")},
            {17, new Pr("Samsung", "C27RG50")},
            {18, new Pr("LG", "24MK600M-B")},
            {19, new Pr("LG", "24GL600F-B")},
            {20, new Pr("Dell", "S3220DGF")},
            {21, new Pr("AOC", "Q3279VWFD8")},
            {22, new Pr("Viewsonic", "VX2458-C-mhd")},
            {23, new Pr("Samsung", "UJ590")},
            {24, new Pr("Asus", "VP249QGR")},
            {25, new Pr("Dell", "UltraSharp U2719DC Black")},
            {26, new Pr("LG", "32UK550-B")},
            {27, new Pr("Dell", "SE2419HR")},
            {28, new Pr("Samsung", "C27F390FHU")},
            {29, new Pr("LG", "24TL510V-PZ")},
            {30, new Pr("AOC", "27V2Q")},
            {31, new Pr("AOC", "24V2Q")},
            {32, new Pr("AOC", "CQ27G2U")},
            {33, new Pr("LG", "28TL510V-PZ")},
            {34, new Pr("LG", "24MK400H-B")},
            {35, new Pr("LG", "27UL650-W")},
            {36, new Pr("LG", "28TL510S-PZ")},
            {37, new Pr("HP", "EliteDisplay E273q")},
            {38, new Pr("AOC", "AG241QX")},
            {39, new Pr("Dell", "P2719H")},
            {40, new Pr("Dell", "P2319H")},
            {41, new Pr("Samsung", "C27F396FHU")},
            {42, new Pr("AOC", "G2590PX")},
            {43, new Pr("LG", "20MK400H-B")},
            {44, new Pr("Dell", "P2418HT")},
            {45, new Pr("Dell", "UltraSharp U2719D")},
            {46, new Pr("Asus", "TUF Gaming VG27WQ")},
            {47, new Pr("Samsung", "LS24R350FHUXEN")},
            {48, new Pr("LG", "22MK600M-B")},
            {49, new Pr("AOC", "G2460VQ6")},
            {50, new Pr("LG", "29UM69G-B")},
            {51, new Pr("Philips", "248E9QHSB")},
            {52, new Pr("LG", "24MP59G-P")},
            {53, new Pr("Dell", "UltraSharp U2412M Black")},
            {54, new Pr("MSI", "Optix MAG271CQR")},
            {55, new Pr("Samsung", "S27E330HZX")},
            {56, new Pr("Philips", "246E9QJAB")},
            {57, new Pr("MSI", "Optix MAG241C")},
            {58, new Pr("Samsung", "S27H850QFU")},
            {59, new Pr("Dell", "P2419H")},
            {60, new Pr("Zowie", "XL2411P")},
            {61, new Pr("Samsung", "C24F396FHU")},
            {62, new Pr("Asus", "Rog Strix XG32VQ")},
            {63, new Pr("Dell", "U2419H")},
            {64, new Pr("LG", "27GL850-B")},
            {65, new Pr("Samsung", "C24RG50")},
            {66, new Pr("Dell", "SE2219H")},
            {67, new Pr("AOC", "G2778VQ")},
            {68, new Pr("AOC", "24G2U5")},
            {69, new Pr("Viewsonic", "Elite XG2405")},
            {70, new Pr("Viewsonic", "VX2758-PC-MH")},
            {71, new Pr("LG", "27MK400H-B")},
            {72, new Pr("LG", "34GL750-B")},
            {73, new Pr("LG", "32ML600M-B")},
            {74, new Pr("Dell", "P2418D")},
            {75, new Pr("Philips", "223V7QHAB")},
            {76, new Pr("Dell", "Ultrasharp U2520D")},
            {77, new Pr("LG", "32MP58HQ")},
            {78, new Pr("Samsung", "U32R590")},
            {79, new Pr("Samsung", "S27F350FHU")},
            {80, new Pr("AOC", "G2590FX")},
            {81, new Pr("LG", "22MK430H-B")},
            {82, new Pr("Samsung", "S24F356FHU")},
            {83, new Pr("LG", "29WK600-W")},
            {84, new Pr("Samsung", "S22F350FHU")},
            {85, new Pr("LG", "27MK600M-B")},
            {86, new Pr("Samsung", "C32HG70")},
            {87, new Pr("Samsung", "LC32JG50QQU")},
            {88, new Pr("AOC", "U2879VF")},
            {89, new Pr("Asus", "MG248QR")},
            {90, new Pr("Asus", "TUF Gaming VG32VQ")},
            {91, new Pr("Asus", "VG278QR")},
            {92, new Pr("Asus", "VG248QG")},
            {93, new Pr("Samsung", "S27R750Q")},
            {94, new Pr("AOC", "27G2U/BK")},
            {95, new Pr("Dell", "UltraSharp U2415")},
            {96, new Pr("Dell", "UltraSharp U2518D")},
            {97, new Pr("Samsung", "U32H850")},
            {98, new Pr("Dell", "UltraSharp U3419W")},
            {99, new Pr("Asus", "VZ249HE Black")},
            {100, new Pr("Dell", "SE2717H")},
            {101, new Pr("Dell", "SE2416H")},
            {102, new Pr("Samsung", "C27F591FDU")},
            {103, new Pr("Samsung", "C43J890")},
            {104, new Pr("LG", "22MK400H-B")},
            {105, new Pr("Asus", "Designo Curve MX34VQ")},
            {106, new Pr("AOC", "G2260VWQ6")},
            {107, new Pr("LG", "29WK500-P")},
            {108, new Pr("Dell", "P4317Q")},
            {109, new Pr("Viewsonic", "VX2758-2KP-mhd")},
            {110, new Pr("Dell", "Alienware AW2518H")},
            {111, new Pr("Dell", "E2420H")},
            {112, new Pr("Dell", "Alienware AW3418DW")},
            {113, new Pr("MSI", "Optix G27CQ4")},
            {114, new Pr("Dell", "Alienware AW3420DW")},
            {115, new Pr("LG", "27MP59G-P")},
            {116, new Pr("Samsung", "LS24E65UPL")},
            {117, new Pr("Acer", "Nitro VG270UP")},
            {118, new Pr("LG", "27MK430H-B")},
            {119, new Pr("Zowie", "Rl2455")},
            {120, new Pr("LG", "24MK430H-B")},
            {121, new Pr("Samsung", "C34H890")},
            {122, new Pr("Philips", "243V7QDAB")},
            {123, new Pr("LG", "34WK650-W")},
            {124, new Pr("Dell", "P2421D")},
            {125, new Pr("Asus", "Rog Swift PG348Q")},
            {126, new Pr("LG", "25UM58-P")},
            {127, new Pr("MSI", "Optix G27C4")},
            {128, new Pr("Asus", "Rog Strix XG27VQ")},
            {129, new Pr("Samsung", "UR55")},
            {130, new Pr("Samsung", "27CHG70")},
            {131, new Pr("AOC", "Q27G2U")},
            {132, new Pr("Dell", "Ultrasharp U2720Q")},
            {133, new Pr("Viewsonic", "VA2419-SH")},
            {134, new Pr("LG", "34UC79G-B")},
            {135, new Pr("Dell", "P2419HC")},
            {136, new Pr("Viewsonic", "XG2402")},
            {137, new Pr("Gigabyte", "Aorus FI27Q")},
            {138, new Pr("LG", "24TL510V-WZ")},
            {139, new Pr("AOC", "e970Swn")},
            {140, new Pr("LG", "29UC88")},
            {141, new Pr("HP", "24f")},
            {142, new Pr("Philips", "276E9QDSB")},
            {143, new Pr("AOC", "AG241QG")},
            {144, new Pr("Gigabyte", "Aorus CV27Q")},
            {145, new Pr("AOC", "E2470SWH")},
            {146, new Pr("Dell", "P2720DC")},
            {147, new Pr("AOC", "Q3279VWF")},
            {148, new Pr("Dell", "P2720D")},
            {149, new Pr("Asus", "ROG Swift PG27VQ")},
            {150, new Pr("Dell", "P2417H")},
            {151, new Pr("Dell", "P2421DC")},
            {152, new Pr("LG", "28TL510V-WZ")},
            {153, new Pr("Dell", "UltraSharp UP3216Q")},
            {154, new Pr("HP", "27fw")},
            {155, new Pr("AOC", "I2481FXH")},
            {156, new Pr("AOC", "CU34G2X")},
            {157, new Pr("LG", "24GL650-B")},
            {158, new Pr("HP", "EliteDisplay E243")},
            {159, new Pr("MSI", "Optix MAG271CV")},
            {160, new Pr("HP", "24w")},
            {161, new Pr("Samsung", "S27R350FHU")},
            {162, new Pr("Philips", "243V7QDSB")},
            {163, new Pr("Dell", "P2719HC")},
            {164, new Pr("Samsung", "C27FG73")},
            {165, new Pr("LG", "27UD58")},
            {166, new Pr("Asus", "VG278Q")},
            {167, new Pr("LG", "27UL600-W")},
            {168, new Pr("Eizo", "EV2450 Black")},
            {169, new Pr("BenQ", "GW2280")},
            {170, new Pr("AOC", "27G2U5")},
            {171, new Pr("Viewsonic", "VA2719-SH")},
            {172, new Pr("LG", "34UM69G-B")},
            {173, new Pr("Acer", "Nitro VG270")},
            {174, new Pr("Dell", "S2719H")},
            {175, new Pr("Dell", "UltraSharp U3219Q")},
            {176, new Pr("Samsung", "C24FG70")},
            {177, new Pr("LG", "24GM79G")},
            {178, new Pr("Asus", "VG245HE")},
            {179, new Pr("Dell", "E2418HN")},
            {180, new Pr("Asus", "TUF Gaming VG27AQ")},
            {181, new Pr("MSI", "Optix MAG241CV")},
            {182, new Pr("LG", "32GK850G-B")},
            {183, new Pr("HP", "EliteDisplay E273")},
            {184, new Pr("Asus", "VP28UQG")},
            {185, new Pr("AOC", "AGON AG272FCX6")},
            {186, new Pr("Acer", "Nitro VG240Y")},
            {187, new Pr("Samsung", "LS34J550WQU")},
            {188, new Pr("Zowie", "XL2546")},
            {189, new Pr("Samsung", "CR50")},
            {190, new Pr("LG", "24MT49S")},
            {191, new Pr("Dell", "SE2417HG")},
            {192, new Pr("Samsung", "U28H750UQU")},
            {193, new Pr("Asus", "VG245Q")},
            {194, new Pr("Samsung", "CHG90")},
            {195, new Pr("BenQ", "GC2870H")},
            {196, new Pr("LG", "27GL63T-B")},
            {197, new Pr("HP", "Omen X 27")},
            {198, new Pr("Dell", "Alienware AW2720HF")},
            {199, new Pr("Acer", "B277")},
            {200, new Pr("MSI", "Optix G241VC")},
            {201, new Pr("Acer", "BE270UA")},
            {202, new Pr("Asus", "Rog Swift PG258Q")},
            {203, new Pr("Dell", "UltraSharp U3415W")},
            {204, new Pr("Dell", "U2717D")},
            {205, new Pr("Dell", "UltraSharp U2417H")},
            {206, new Pr("AOC", "24P1")},
            {207, new Pr("Samsung", "C24FG73")},
            {208, new Pr("Viewsonic", "VX4380-4K")},
            {209, new Pr("Samsung", "S24H850")},
            {210, new Pr("Acer", "Nitro XV273K")},
            {211, new Pr("AOC", "24E1Q")},
            {212, new Pr("BenQ", "EX3203R")},
            {213, new Pr("MSI", "Prestige PS341WU")},
            {214, new Pr("Samsung", "S24F350FHU")},
            {215, new Pr("Philips", "273V7QJAB")},
            {216, new Pr("Dell", "S2419H")},
            {217, new Pr("Samsung", "C27H711")},
            {218, new Pr("Viewsonic", "VX3276-2K-MHD")},
            {219, new Pr("Viewsonic", "VX3258-2KPC-MHD")},
            {220, new Pr("Asus", "TUF Gaming VG27BQ")},
            {221, new Pr("HP", "P24h G4")},
            {222, new Pr("Acer", "Predator X34P")},
            {223, new Pr("LG", "34UC99-W")},
            {224, new Pr("Asus", "VP28UQGL")},
            {225, new Pr("AOC", "24B1H")},
            {226, new Pr("Samsung", "U28E590D")},
            {227, new Pr("Asus", "VZ239HE Black")},
            {228, new Pr("Acer", "Nitro RG270")},
            {229, new Pr("Asus", "VA249HE")},
            {230, new Pr("Samsung", "C49RG90SSU")},
            {231, new Pr("Iiyama", "ProLite E2783QSU-B1")},
            {232, new Pr("Acer", "Nitro VG271UPbmiipx")},
            {233, new Pr("Viewsonic", "XG2705")},
            {234, new Pr("Asus", "VG248QE")},
            {235, new Pr("Dell", "E2016HV")},
            {236, new Pr("Dell", "UltraSharp U2718Q")},
            {237, new Pr("Philips", "V Line 200V4QSBR")},
            {238, new Pr("Dell", "E2720HS")},
            {239, new Pr("AOC", "AG273QZ")},
        };
        [SetUp]
        public void Setup()
        {
            minHasher = new MinHasher<int>(signatureSize, hashSeeds);
            lshSearcher = new LSHSearch<int>(minHasher, SimilarityMeasures.Jaccard);
        }

        [Test]
        public void SearchTest()
        {
            var searchString = new string[] { "Samsungs", "C24F390FHU" };
            var result = lshSearcher.GetClosest(productList, searchString[1], 5);
            Assert.IsNotEmpty(result);
            Assert.IsNotNull(result);
        }
    }
    public class Pr : ILSHWeightedHashable
    {
        private string Brand;
        private string Model;

        public Pr(string v1, string v2)
        {
            this.Brand = v1;
            this.Model = v2;
        }

        public List<Tuple<string, double>> GetWeightedStringsToHash()
        {
            return new List<Tuple<string, double>>()
            {
                new Tuple<string, double>(Brand, 0.2),
                new Tuple<string, double>(Model, 0.8)
            };
        }
    }
}