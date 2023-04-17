namespace IbkrToSruTests;

using System;
using System.Collections.Immutable;
using System.Globalization;
using System.IO;
using IbkrToSru;

using NUnit.Framework;

public static class SruTest
{
    private static readonly TestCaseData[] CreateCases =
    {
        new(
            new[]
            {
                new Execution("USD", "AEHR", new DateTime(2021, 10, 06, 09, 45, 56), 80, 14.24, -1139.2, -1, 1140.2, 0),
                new Execution("USD", "AEHR", new DateTime(2021, 10, 07, 13, 09, 43), -80, 16.13, 1290.4, -1.01610104, -1140.2, 149.183899),
            },
            """
            #BLANKETT K4-2021P4
            #IDENTITET 197903054524 20220426 133009
            #UPPGIFT 3100 80
            #UPPGIFT 3101 AEHR
            #UPPGIFT 3102 11065
            #UPPGIFT 3103 9785
            #UPPGIFT 3104 1280
            #UPPGIFT 3105 0
            #UPPGIFT 3300 11065
            #UPPGIFT 3301 9785
            #UPPGIFT 3304 1280
            #UPPGIFT 3305 0
            #UPPGIFT 7014 1
            #BLANKETTSLUT
            #FIL_SLUT

            """),
        new(
            new[]
            {
                new Execution("USD", "AMC", new DateTime(2021, 11, 23, 12, 46, 38), 50, 39.83, -1991.5d, -1, 1992.5, 0),
                new Execution("USD", "AMC", new DateTime(2021, 11, 30, 11, 55, 26), -50, 33.8, 1690, -1.014569, -1992.5, -303.514569),
            },
            """
            #BLANKETT K4-2021P4
            #IDENTITET 197903054524 20220426 133009
            #UPPGIFT 3100 50
            #UPPGIFT 3101 AMC
            #UPPGIFT 3102 14494
            #UPPGIFT 3103 17099
            #UPPGIFT 3104 0
            #UPPGIFT 3105 2605
            #UPPGIFT 3300 14494
            #UPPGIFT 3301 17099
            #UPPGIFT 3304 0
            #UPPGIFT 3305 2605
            #UPPGIFT 7014 1
            #BLANKETTSLUT
            #FIL_SLUT

            """),
        new(
            new[]
            {
                new Execution("USD", "AVGO", new DateTime(2021, 12, 10, 09, 36, 08), -10, 624.83, 6248.30, -1.03305633, -6247.26694367, 0),
                new Execution("USD", "AVGO", new DateTime(2021, 12, 10, 09, 39, 30), 10, 632.37, -6323.7, -1, 6247.266944, -77.433055999999993d),
            },
            """
            #BLANKETT K4-2021P4
            #IDENTITET 197903054524 20220426 133009
            #UPPGIFT 3100 10
            #UPPGIFT 3101 AVGO
            #UPPGIFT 3102 53611
            #UPPGIFT 3103 54275
            #UPPGIFT 3104 0
            #UPPGIFT 3105 664
            #UPPGIFT 3300 53611
            #UPPGIFT 3301 54275
            #UPPGIFT 3304 0
            #UPPGIFT 3305 664
            #UPPGIFT 7014 1
            #BLANKETTSLUT
            #FIL_SLUT

            """),
        new(
            new[]
            {
                new Execution("USD", "NAKD", new DateTime(2021, 10, 14, 09, 41, 55), 4091, 0.69375407, -2838.1479, -30.6825, 2868.8304, 0),
                new Execution("USD", "NAKD", new DateTime(2021, 10, 18, 09, 30, 01), -4091, 0.6616, 2706.6056, -28.377888689, -2867.096556904, -188.868845),
            },
            """
            #BLANKETT K4-2021P4
            #IDENTITET 197903054524 20220426 133009
            #UPPGIFT 3100 4091
            #UPPGIFT 3101 NAKD
            #UPPGIFT 3102 22983
            #UPPGIFT 3103 24604
            #UPPGIFT 3104 0
            #UPPGIFT 3105 1621
            #UPPGIFT 3300 22983
            #UPPGIFT 3301 24604
            #UPPGIFT 3304 0
            #UPPGIFT 3305 1621
            #UPPGIFT 7014 1
            #BLANKETTSLUT
            #FIL_SLUT

            """),
        new(
            new[]
            {
                new Execution("USD", "TRT", new DateTime(2021, 12, 10, 15, 14, 57), -329, 8.5, 2796.5, -2.52091315, -2865.9275, -71.948414),
                new Execution("USD", "TTD", new DateTime(2021, 11, 30, 9, 31, 17), 18, 109.6, -1972.8, -1, 1973.8, 0),
                new Execution("USD", "TTD", new DateTime(2021, 11, 30, 12, 6, 4), -18, 102.13, 1838.34, -1.011517534, -1973.8, -136.471518),
                new Execution("USD", "UEC", new DateTime(2021, 11, 30, 14, 44, 10), 507, 3.825, -1939.275, -3.8025, 1943.0775, 0),
                new Execution("USD", "UEC", new DateTime(2021, 12, 1, 12, 57, 19), -507, 3.7, 1875.9, -3.87240009, -1943.0775, -71.0499),
                new Execution("USD", "UONEK", new DateTime(2021, 10, 13, 9, 37, 24), 394, 7.177461929, -2827.92, -2.955, 2830.875, 0),
                new Execution("USD", "UONEK", new DateTime(2021, 10, 13, 10, 22, 36), -394, 7.1, 2797.4, -3.01615274, -2830.875, -36.491153),
                new Execution("USD", "USIO", new DateTime(2021, 11, 8, 11, 15, 30), 85, 6.08, -516.8, -1, 517.8, 0),
                new Execution("USD", "USIO", new DateTime(2021, 11, 8, 11, 38, 10), -85, 5.9, 501.5, -1.01267265, -517.8, -17.312673),
                new Execution("USD", "VERA", new DateTime(2021, 12, 7, 14, 6, 23), 30, 29.5, -885, -1, 886, 0),
                new Execution("USD", "VERA", new DateTime(2021, 12, 8, 9, 53, 52), -30, 27.64, 829.2, -1.00779892, -886, -57.807799),
                new Execution("USD", "VERB", new DateTime(2021, 10, 27, 12, 53, 12), 1242, 2.11, -2620.62, -9.315, 2629.935, 0),
                new Execution("USD", "VERB", new DateTime(2021, 10, 27, 15, 53, 40), -1242, 2.090402576, 2596.28, -9.476039028, -2629.935, -43.131039),
                new Execution("USD", "VERI", new DateTime(2021, 10, 19, 12, 24, 55), 113, 23.7, -2678.1, -1, 2679.1, 0),
                new Execution("USD", "VERI", new DateTime(2021, 10, 22, 10, 29, 22), -56, 28.01, 1568.56, -1.014663656, -1328.2, 239.345336),
                new Execution("USD", "VERI", new DateTime(2021, 11, 9, 9, 58, 50), -57, 29.9, 1704.3, -1.01547493, -1350.9, 352.384525),
                new Execution("USD", "VERI", new DateTime(2021, 11, 17, 12, 42, 43), 81, 26.3, -2130.3, -1, 2131.3, 0),
                new Execution("USD", "VERI", new DateTime(2021, 11, 23, 9, 31, 42), -81, 26, 2106, -1.0203796, -2131.3, -26.320379),
                new Execution("USD", "VIR", new DateTime(2021, 12, 3, 9, 39, 22), 39, 47.97, -1870.83, -1, 1871.83, 0),
                new Execution("USD", "VIR", new DateTime(2021, 12, 3, 10, 1, 57), -39, 45.77, 1785.03, -1.013744653, -1871.83, -87.813745),
                new Execution("USD", "VRAR", new DateTime(2021, 11, 8, 9, 33, 55), 343, 13.43, -4606.49, -2.5725, 4609.0625, 0),
                new Execution("USD", "VRAR", new DateTime(2021, 11, 10, 15, 6, 2), -343, 13.61, 4668.23, -2.637124973, -4609.0625, 56.530375),
                new Execution("USD", "VRAR", new DateTime(2021, 11, 22, 11, 9, 25), 134, 15.485074627, -2075, -1.005, 2076.005, 0),
                new Execution("USD", "VRAR", new DateTime(2021, 11, 22, 11, 9, 26), -134, 15.307462687, 2051.2, -1.03140712, -2076.005, -25.836407),
                new Execution("USD", "VUZI", new DateTime(2021, 11, 8, 15, 48, 32), 384, 12.07, -4634.88, -2.88, 4637.76, 0),
                new Execution("USD", "VUZI", new DateTime(2021, 11, 10, 15, 59, 5), -384, 12.271302083, 4712.18, -2.949728118, -4637.76, 71.470271),
                new Execution("USD", "VUZI", new DateTime(2021, 11, 18, 15, 54, 1), 158, 13.316835443, -2104.06, -1.185, 2105.245, 0),
                new Execution("USD", "VUZI", new DateTime(2021, 11, 19, 13, 35, 49), -158, 13, 2054, -1.2142774, -2105.245001, -52.459278),
                new Execution("USD", "WEI", new DateTime(2021, 11, 10, 14, 30, 12), 378, 0.9101, -344.0178, -2.835, 346.8528, 0),
                new Execution("USD", "WEI", new DateTime(2021, 11, 12, 10, 9, 48), -378, 0.8935, 337.743, -2.881704489, -346.8528, -11.991505),
                new Execution("USD", "WIMI", new DateTime(2021, 11, 17, 12, 29, 50), 450, 4.83, -2173.5, -3.375, 2176.875, 0),
                new Execution("USD", "WIMI", new DateTime(2021, 11, 17, 15, 48, 5), -450, 4.67, 2101.5, -3.43926765, -2176.875, -78.814268),
                new Execution("USD", "XPEV", new DateTime(2021, 11, 5, 9, 43, 19), 49, 46.89, -2297.61, -1, 2298.61, 0),
                new Execution("USD", "XPEV", new DateTime(2021, 11, 5, 15, 59, 1), -49, 46.445, 2275.805, -1.017437606, -2298.61, -23.822438),
                new Execution("USD", "XPEV", new DateTime(2021, 11, 18, 9, 49, 43), 45, 47.01, -2115.45, -1, 2116.45, 0),
                new Execution("USD", "XPEV", new DateTime(2021, 12, 3, 9, 31, 4), -45, 47.34, 2130.3, -1.01621953, -2116.45, 12.83378),
                new Execution("USD", "ZM", new DateTime(2021, 10, 13, 9, 55, 29), 10, 262.229, -2622.29, -1, 2623.29, 0),
                new Execution("USD", "ZM", new DateTime(2021, 10, 14, 15, 30, 14), -10, 268.05, 2680.5, -1.01486055, -2623.29, 56.19514),
            },
            """
            #BLANKETT K4-2021P4
            #IDENTITET 197903054524 20220426 133009
            #UPPGIFT 3100 329
            #UPPGIFT 3101 TRT
            #UPPGIFT 3102 23977
            #UPPGIFT 3103 24594
            #UPPGIFT 3104 0
            #UPPGIFT 3105 617
            #UPPGIFT 3110 18
            #UPPGIFT 3111 TTD
            #UPPGIFT 3112 15767
            #UPPGIFT 3113 16938
            #UPPGIFT 3114 0
            #UPPGIFT 3115 1171
            #UPPGIFT 3120 507
            #UPPGIFT 3121 UEC
            #UPPGIFT 3122 16065
            #UPPGIFT 3123 16675
            #UPPGIFT 3124 0
            #UPPGIFT 3125 610
            #UPPGIFT 3130 394
            #UPPGIFT 3131 UONEK
            #UPPGIFT 3132 23980
            #UPPGIFT 3133 24293
            #UPPGIFT 3134 0
            #UPPGIFT 3135 313
            #UPPGIFT 3140 85
            #UPPGIFT 3141 USIO
            #UPPGIFT 3142 4295
            #UPPGIFT 3143 4444
            #UPPGIFT 3144 0
            #UPPGIFT 3145 149
            #UPPGIFT 3150 30
            #UPPGIFT 3151 VERA
            #UPPGIFT 3152 7107
            #UPPGIFT 3153 7603
            #UPPGIFT 3154 0
            #UPPGIFT 3155 496
            #UPPGIFT 3160 1242
            #UPPGIFT 3161 VERB
            #UPPGIFT 3162 22199
            #UPPGIFT 3163 22569
            #UPPGIFT 3164 0
            #UPPGIFT 3165 370
            #UPPGIFT 3170 56
            #UPPGIFT 3171 VERI
            #UPPGIFT 3172 13452
            #UPPGIFT 3173 11398
            #UPPGIFT 3174 2054
            #UPPGIFT 3175 0
            #UPPGIFT 3180 57
            #UPPGIFT 3181 VERI
            #UPPGIFT 3182 14617
            #UPPGIFT 3183 11593
            #UPPGIFT 3184 3024
            #UPPGIFT 3185 0
            #UPPGIFT 3300 141459
            #UPPGIFT 3301 140107
            #UPPGIFT 3304 5078
            #UPPGIFT 3305 3726
            #UPPGIFT 7014 1
            #BLANKETTSLUT
            #BLANKETT K4-2021P4
            #IDENTITET 197903054524 20220426 133009
            #UPPGIFT 3100 81
            #UPPGIFT 3101 VERI
            #UPPGIFT 3102 18064
            #UPPGIFT 3103 18290
            #UPPGIFT 3104 0
            #UPPGIFT 3105 226
            #UPPGIFT 3110 39
            #UPPGIFT 3111 VIR
            #UPPGIFT 3112 15310
            #UPPGIFT 3113 16063
            #UPPGIFT 3114 0
            #UPPGIFT 3115 754
            #UPPGIFT 3120 343
            #UPPGIFT 3121 VRAR
            #UPPGIFT 3122 40038
            #UPPGIFT 3123 39553
            #UPPGIFT 3124 485
            #UPPGIFT 3125 0
            #UPPGIFT 3130 134
            #UPPGIFT 3131 VRAR
            #UPPGIFT 3132 17594
            #UPPGIFT 3133 17815
            #UPPGIFT 3134 0
            #UPPGIFT 3135 222
            #UPPGIFT 3140 384
            #UPPGIFT 3141 VUZI
            #UPPGIFT 3142 40412
            #UPPGIFT 3143 39799
            #UPPGIFT 3144 613
            #UPPGIFT 3145 0
            #UPPGIFT 3150 158
            #UPPGIFT 3151 VUZI
            #UPPGIFT 3152 17616
            #UPPGIFT 3153 18066
            #UPPGIFT 3154 0
            #UPPGIFT 3155 450
            #UPPGIFT 3160 378
            #UPPGIFT 3161 WEI
            #UPPGIFT 3162 2874
            #UPPGIFT 3163 2977
            #UPPGIFT 3164 0
            #UPPGIFT 3165 103
            #UPPGIFT 3170 450
            #UPPGIFT 3171 WIMI
            #UPPGIFT 3172 18005
            #UPPGIFT 3173 18681
            #UPPGIFT 3174 0
            #UPPGIFT 3175 676
            #UPPGIFT 3180 49
            #UPPGIFT 3181 XPEV
            #UPPGIFT 3182 19521
            #UPPGIFT 3183 19726
            #UPPGIFT 3184 0
            #UPPGIFT 3185 204
            #UPPGIFT 3300 189434
            #UPPGIFT 3301 190970
            #UPPGIFT 3304 1098
            #UPPGIFT 3305 2635
            #UPPGIFT 7014 2
            #BLANKETTSLUT
            #BLANKETT K4-2021P4
            #IDENTITET 197903054524 20220426 133009
            #UPPGIFT 3100 45
            #UPPGIFT 3101 XPEV
            #UPPGIFT 3102 18272
            #UPPGIFT 3103 18162
            #UPPGIFT 3104 110
            #UPPGIFT 3105 0
            #UPPGIFT 3110 10
            #UPPGIFT 3111 ZM
            #UPPGIFT 3112 22994
            #UPPGIFT 3113 22512
            #UPPGIFT 3114 482
            #UPPGIFT 3115 0
            #UPPGIFT 3300 41266
            #UPPGIFT 3301 40674
            #UPPGIFT 3304 592
            #UPPGIFT 3305 0
            #UPPGIFT 7014 3
            #BLANKETTSLUT
            #FIL_SLUT
            
            """),
    };

    [TestCaseSource(nameof(CreateCases))]
    public static void Create(Execution[] executions, string expected)
    {
        var actual = Sru.Create(
            executions.ToImmutableArray(),
            2021,
            new ExchangeRate("USD", 8.5815),
            "19790305-4524",
            new DateTime(2022, 04, 26, 13, 30, 09));
        Assert.AreEqual(expected, actual);
    }

    private static readonly TestCaseData[] CreateMergedBySymbolCases =
    {
        new(
            new[]
            {
                new Execution("USD", "AEHR", new DateTime(2021, 10, 06, 09, 45, 56), 80, 14.24, -1139.2, -1, 1140.2, 0),
                new Execution("USD", "AEHR", new DateTime(2021, 10, 07, 13, 09, 43), -80, 16.13, 1290.4, -1.01610104, -1140.2, 149.183899),
            },
            """
            #BLANKETT K4-2021P4
            #IDENTITET 197903054524 20220426 133009
            #UPPGIFT 3100 80
            #UPPGIFT 3101 AEHR
            #UPPGIFT 3102 11065
            #UPPGIFT 3103 9785
            #UPPGIFT 3104 1280
            #UPPGIFT 3105 0
            #UPPGIFT 3300 11065
            #UPPGIFT 3301 9785
            #UPPGIFT 3304 1280
            #UPPGIFT 3305 0
            #UPPGIFT 7014 1
            #BLANKETTSLUT
            #FIL_SLUT

            """),
        new(
            new[]
            {
                new Execution("USD", "AMC", new DateTime(2021, 11, 23, 12, 46, 38), 50, 39.83, -1991.5d, -1, 1992.5, 0),
                new Execution("USD", "AMC", new DateTime(2021, 11, 30, 11, 55, 26), -50, 33.8, 1690, -1.014569, -1992.5, -303.514569),
            },
            """
            #BLANKETT K4-2021P4
            #IDENTITET 197903054524 20220426 133009
            #UPPGIFT 3100 50
            #UPPGIFT 3101 AMC
            #UPPGIFT 3102 14494
            #UPPGIFT 3103 17099
            #UPPGIFT 3104 0
            #UPPGIFT 3105 2605
            #UPPGIFT 3300 14494
            #UPPGIFT 3301 17099
            #UPPGIFT 3304 0
            #UPPGIFT 3305 2605
            #UPPGIFT 7014 1
            #BLANKETTSLUT
            #FIL_SLUT

            """),
        new(
            new[]
            {
                new Execution("USD", "AVGO", new DateTime(2021, 12, 10, 09, 36, 08), -10, 624.83, 6248.30, -1.03305633, -6247.26694367, 0),
                new Execution("USD", "AVGO", new DateTime(2021, 12, 10, 09, 39, 30), 10, 632.37, -6323.7, -1, 6247.266944, -77.433055999999993d),
            },
            """
            #BLANKETT K4-2021P4
            #IDENTITET 197903054524 20220426 133009
            #UPPGIFT 3100 10
            #UPPGIFT 3101 AVGO
            #UPPGIFT 3102 53611
            #UPPGIFT 3103 54275
            #UPPGIFT 3104 0
            #UPPGIFT 3105 664
            #UPPGIFT 3300 53611
            #UPPGIFT 3301 54275
            #UPPGIFT 3304 0
            #UPPGIFT 3305 664
            #UPPGIFT 7014 1
            #BLANKETTSLUT
            #FIL_SLUT

            """),
        new(
            new[]
            {
                new Execution("USD", "NAKD", new DateTime(2021, 10, 14, 09, 41, 55), 4091, 0.69375407, -2838.1479, -30.6825, 2868.8304, 0),
                new Execution("USD", "NAKD", new DateTime(2021, 10, 18, 09, 30, 01), -4091, 0.6616, 2706.6056, -28.377888689, -2867.096556904, -188.868845),
            },
            """
            #BLANKETT K4-2021P4
            #IDENTITET 197903054524 20220426 133009
            #UPPGIFT 3100 4091
            #UPPGIFT 3101 NAKD
            #UPPGIFT 3102 22983
            #UPPGIFT 3103 24604
            #UPPGIFT 3104 0
            #UPPGIFT 3105 1621
            #UPPGIFT 3300 22983
            #UPPGIFT 3301 24604
            #UPPGIFT 3304 0
            #UPPGIFT 3305 1621
            #UPPGIFT 7014 1
            #BLANKETTSLUT
            #FIL_SLUT

            """),
        new(
            new[]
            {
                new Execution("USD", "TRT", new DateTime(2021, 12, 10, 15, 14, 57), -329, 8.5, 2796.5, -2.52091315, -2865.9275, -71.948414),
                new Execution("USD", "TTD", new DateTime(2021, 11, 30, 9, 31, 17), 18, 109.6, -1972.8, -1, 1973.8, 0),
                new Execution("USD", "TTD", new DateTime(2021, 11, 30, 12, 6, 4), -18, 102.13, 1838.34, -1.011517534, -1973.8, -136.471518),
                new Execution("USD", "UEC", new DateTime(2021, 11, 30, 14, 44, 10), 507, 3.825, -1939.275, -3.8025, 1943.0775, 0),
                new Execution("USD", "UEC", new DateTime(2021, 12, 1, 12, 57, 19), -507, 3.7, 1875.9, -3.87240009, -1943.0775, -71.0499),
                new Execution("USD", "UONEK", new DateTime(2021, 10, 13, 9, 37, 24), 394, 7.177461929, -2827.92, -2.955, 2830.875, 0),
                new Execution("USD", "UONEK", new DateTime(2021, 10, 13, 10, 22, 36), -394, 7.1, 2797.4, -3.01615274, -2830.875, -36.491153),
                new Execution("USD", "USIO", new DateTime(2021, 11, 8, 11, 15, 30), 85, 6.08, -516.8, -1, 517.8, 0),
                new Execution("USD", "USIO", new DateTime(2021, 11, 8, 11, 38, 10), -85, 5.9, 501.5, -1.01267265, -517.8, -17.312673),
                new Execution("USD", "VERA", new DateTime(2021, 12, 7, 14, 6, 23), 30, 29.5, -885, -1, 886, 0),
                new Execution("USD", "VERA", new DateTime(2021, 12, 8, 9, 53, 52), -30, 27.64, 829.2, -1.00779892, -886, -57.807799),
                new Execution("USD", "VERB", new DateTime(2021, 10, 27, 12, 53, 12), 1242, 2.11, -2620.62, -9.315, 2629.935, 0),
                new Execution("USD", "VERB", new DateTime(2021, 10, 27, 15, 53, 40), -1242, 2.090402576, 2596.28, -9.476039028, -2629.935, -43.131039),
                new Execution("USD", "VERI", new DateTime(2021, 10, 19, 12, 24, 55), 113, 23.7, -2678.1, -1, 2679.1, 0),
                new Execution("USD", "VERI", new DateTime(2021, 10, 22, 10, 29, 22), -56, 28.01, 1568.56, -1.014663656, -1328.2, 239.345336),
                new Execution("USD", "VERI", new DateTime(2021, 11, 9, 9, 58, 50), -57, 29.9, 1704.3, -1.01547493, -1350.9, 352.384525),
                new Execution("USD", "VERI", new DateTime(2021, 11, 17, 12, 42, 43), 81, 26.3, -2130.3, -1, 2131.3, 0),
                new Execution("USD", "VERI", new DateTime(2021, 11, 23, 9, 31, 42), -81, 26, 2106, -1.0203796, -2131.3, -26.320379),
                new Execution("USD", "VIR", new DateTime(2021, 12, 3, 9, 39, 22), 39, 47.97, -1870.83, -1, 1871.83, 0),
                new Execution("USD", "VIR", new DateTime(2021, 12, 3, 10, 1, 57), -39, 45.77, 1785.03, -1.013744653, -1871.83, -87.813745),
                new Execution("USD", "VRAR", new DateTime(2021, 11, 8, 9, 33, 55), 343, 13.43, -4606.49, -2.5725, 4609.0625, 0),
                new Execution("USD", "VRAR", new DateTime(2021, 11, 10, 15, 6, 2), -343, 13.61, 4668.23, -2.637124973, -4609.0625, 56.530375),
                new Execution("USD", "VRAR", new DateTime(2021, 11, 22, 11, 9, 25), 134, 15.485074627, -2075, -1.005, 2076.005, 0),
                new Execution("USD", "VRAR", new DateTime(2021, 11, 22, 11, 9, 26), -134, 15.307462687, 2051.2, -1.03140712, -2076.005, -25.836407),
                new Execution("USD", "VUZI", new DateTime(2021, 11, 8, 15, 48, 32), 384, 12.07, -4634.88, -2.88, 4637.76, 0),
                new Execution("USD", "VUZI", new DateTime(2021, 11, 10, 15, 59, 5), -384, 12.271302083, 4712.18, -2.949728118, -4637.76, 71.470271),
                new Execution("USD", "VUZI", new DateTime(2021, 11, 18, 15, 54, 1), 158, 13.316835443, -2104.06, -1.185, 2105.245, 0),
                new Execution("USD", "VUZI", new DateTime(2021, 11, 19, 13, 35, 49), -158, 13, 2054, -1.2142774, -2105.245001, -52.459278),
                new Execution("USD", "WEI", new DateTime(2021, 11, 10, 14, 30, 12), 378, 0.9101, -344.0178, -2.835, 346.8528, 0),
                new Execution("USD", "WEI", new DateTime(2021, 11, 12, 10, 9, 48), -378, 0.8935, 337.743, -2.881704489, -346.8528, -11.991505),
                new Execution("USD", "WIMI", new DateTime(2021, 11, 17, 12, 29, 50), 450, 4.83, -2173.5, -3.375, 2176.875, 0),
                new Execution("USD", "WIMI", new DateTime(2021, 11, 17, 15, 48, 5), -450, 4.67, 2101.5, -3.43926765, -2176.875, -78.814268),
                new Execution("USD", "XPEV", new DateTime(2021, 11, 5, 9, 43, 19), 49, 46.89, -2297.61, -1, 2298.61, 0),
                new Execution("USD", "XPEV", new DateTime(2021, 11, 5, 15, 59, 1), -49, 46.445, 2275.805, -1.017437606, -2298.61, -23.822438),
                new Execution("USD", "XPEV", new DateTime(2021, 11, 18, 9, 49, 43), 45, 47.01, -2115.45, -1, 2116.45, 0),
                new Execution("USD", "XPEV", new DateTime(2021, 12, 3, 9, 31, 4), -45, 47.34, 2130.3, -1.01621953, -2116.45, 12.83378),
                new Execution("USD", "ZM", new DateTime(2021, 10, 13, 9, 55, 29), 10, 262.229, -2622.29, -1, 2623.29, 0),
                new Execution("USD", "ZM", new DateTime(2021, 10, 14, 15, 30, 14), -10, 268.05, 2680.5, -1.01486055, -2623.29, 56.19514),
            },
            """
            #BLANKETT K4-2021P4
            #IDENTITET 197903054524 20220426 133009
            #UPPGIFT 3100 329
            #UPPGIFT 3101 TRT
            #UPPGIFT 3102 23977
            #UPPGIFT 3103 24594
            #UPPGIFT 3104 0
            #UPPGIFT 3105 617
            #UPPGIFT 3110 18
            #UPPGIFT 3111 TTD
            #UPPGIFT 3112 15767
            #UPPGIFT 3113 16938
            #UPPGIFT 3114 0
            #UPPGIFT 3115 1171
            #UPPGIFT 3120 507
            #UPPGIFT 3121 UEC
            #UPPGIFT 3122 16065
            #UPPGIFT 3123 16675
            #UPPGIFT 3124 0
            #UPPGIFT 3125 610
            #UPPGIFT 3130 394
            #UPPGIFT 3131 UONEK
            #UPPGIFT 3132 23980
            #UPPGIFT 3133 24293
            #UPPGIFT 3134 0
            #UPPGIFT 3135 313
            #UPPGIFT 3140 85
            #UPPGIFT 3141 USIO
            #UPPGIFT 3142 4295
            #UPPGIFT 3143 4444
            #UPPGIFT 3144 0
            #UPPGIFT 3145 149
            #UPPGIFT 3150 30
            #UPPGIFT 3151 VERA
            #UPPGIFT 3152 7107
            #UPPGIFT 3153 7603
            #UPPGIFT 3154 0
            #UPPGIFT 3155 496
            #UPPGIFT 3160 1242
            #UPPGIFT 3161 VERB
            #UPPGIFT 3162 22199
            #UPPGIFT 3163 22569
            #UPPGIFT 3164 0
            #UPPGIFT 3165 370
            #UPPGIFT 3170 194
            #UPPGIFT 3171 VERI
            #UPPGIFT 3172 46133
            #UPPGIFT 3173 41280
            #UPPGIFT 3174 4852
            #UPPGIFT 3175 0
            #UPPGIFT 3180 39
            #UPPGIFT 3181 VIR
            #UPPGIFT 3182 15310
            #UPPGIFT 3183 16063
            #UPPGIFT 3184 0
            #UPPGIFT 3185 754
            #UPPGIFT 3300 174833
            #UPPGIFT 3301 174459
            #UPPGIFT 3304 4852
            #UPPGIFT 3305 4480
            #UPPGIFT 7014 1
            #BLANKETTSLUT
            #BLANKETT K4-2021P4
            #IDENTITET 197903054524 20220426 133009
            #UPPGIFT 3100 477
            #UPPGIFT 3101 VRAR
            #UPPGIFT 3102 57631
            #UPPGIFT 3103 57368
            #UPPGIFT 3104 263
            #UPPGIFT 3105 0
            #UPPGIFT 3110 542
            #UPPGIFT 3111 VUZI
            #UPPGIFT 3112 58028
            #UPPGIFT 3113 57865
            #UPPGIFT 3114 163
            #UPPGIFT 3115 0
            #UPPGIFT 3120 378
            #UPPGIFT 3121 WEI
            #UPPGIFT 3122 2874
            #UPPGIFT 3123 2977
            #UPPGIFT 3124 0
            #UPPGIFT 3125 103
            #UPPGIFT 3130 450
            #UPPGIFT 3131 WIMI
            #UPPGIFT 3132 18005
            #UPPGIFT 3133 18681
            #UPPGIFT 3134 0
            #UPPGIFT 3135 676
            #UPPGIFT 3140 94
            #UPPGIFT 3141 XPEV
            #UPPGIFT 3142 37794
            #UPPGIFT 3143 37888
            #UPPGIFT 3144 0
            #UPPGIFT 3145 94
            #UPPGIFT 3150 10
            #UPPGIFT 3151 ZM
            #UPPGIFT 3152 22994
            #UPPGIFT 3153 22512
            #UPPGIFT 3154 482
            #UPPGIFT 3155 0
            #UPPGIFT 3300 197326
            #UPPGIFT 3301 197291
            #UPPGIFT 3304 908
            #UPPGIFT 3305 873
            #UPPGIFT 7014 2
            #BLANKETTSLUT
            #FIL_SLUT
            
            """),
    };

    [TestCaseSource(nameof(CreateMergedBySymbolCases))]
    public static void CreateMergedBySymbol(Execution[] executions, string expected)
    {
        var actual = Sru.CreateMergedBySymbol(
            executions.ToImmutableArray(),
            2021,
            new ExchangeRate("USD", 8.5815),
            "19790305-4524",
            new DateTime(2022, 04, 26, 13, 30, 09));
        Assert.AreEqual(expected, actual);
    }

    [Explicit]
    [TestCase(@"C:\Temp\U1234567_20210104_20211231.csv")]
    public static void Dump(string fileName)
    {
        var executions = Csv.ReadIbkr(File.ReadAllText(fileName));
        foreach (var execution in executions)
        {
            Console.WriteLine($"new Execution(\"{execution.Currency}\", \"{execution.Symbol}\", new DateTime({execution.Time.Year}, {execution.Time.Month}, {execution.Time.Day}, {execution.Time.Hour}, {execution.Time.Minute}, {execution.Time.Second}), {execution.Quantity.ToString(CultureInfo.InvariantCulture)}, {execution.Price.ToString(CultureInfo.InvariantCulture)}, {execution.Proceeds.ToString(CultureInfo.InvariantCulture)}, {execution.Fee.ToString(CultureInfo.InvariantCulture)}, {execution.Basis.ToString(CultureInfo.InvariantCulture)}, {execution.Pnl.ToString(CultureInfo.InvariantCulture)}),");
        }
    }
}
