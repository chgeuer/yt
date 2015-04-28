﻿namespace Downloader
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.IO;
    using System.Linq;
    using System.Net;
    using System.Net.Http;
    using System.Text;
    using NVK = System.Collections.Generic.Dictionary<string, string>;

    class Program
    {
        static void Main(string[] args)
        {
            var addresses = new[] {
                //"https://www.youtube.com/watch?v=VQAAkO5B5Hg+",
                //"https://www.youtube.com/watch?v=Q5POuMHxW-0",
                //"https://www.youtube.com/watch?v=pts6F00GFuU",
                // "https://www.youtube.com/watch?v=hSoQ_Cyse2A"
                "https://www.youtube.com/watch?v=xrYRH3PYYT0"
            };

            addresses.ToList().ForEach(D);
        }

        static void D(string a)
        {
            Func<string, string> cachedDownload = id =>
            {
                string content;
                var cacheFile = new FileInfo(string.Format("{0}.txt", id)).FullName;
                if (File.Exists(cacheFile))
                {
                    Console.WriteLine("Read cache file {0}", cacheFile);
                    content = File.ReadAllText(cacheFile);
                }
                else
                {
                    Console.WriteLine("Wrote cache file {0}", cacheFile);

                    content = YoutubeMovieData.Download(id);
                    File.WriteAllText(cacheFile, content);
                }

                return content;
            };

            Func<string, string> fixedFile = id => { return "no_get_video_log=1&caption_tracks=lc%3Den%26n%3DEnglisch%2B%2528Automatisch%2Berzeugt%2529%26k%3Dasr%26t%3D1%26u%3Dhttp%253A%252F%252Fwww.youtube.com%252Fapi%252Ftimedtext%253Fsparams%253Dasr_langs%25252Ccaps%25252Cv%25252Cexpire%2526expire%253D1430065572%2526hl%253Dde_DE%2526asr_langs%253Dde%25252Cen%25252Cko%25252Cru%25252Cpt%25252Cja%25252Cit%25252Ces%25252Cnl%25252Cfr%2526signature%253D379A46C189B0BFF4157662E5A314DC349F82D159.7878262566FA6DAB03A3DBA0312B6D828E297465%2526key%253Dyttt1%2526caps%253Dasr%2526v%253Dpts6F00GFuU%2526kind%253Dasr%2526lang%253Den%26v%3Da.en&url_encoded_fmt_stream_map=itag%3D22%26url%3Dhttp%253A%252F%252Fr8---sn-5hn7ym7s.googlevideo.com%252Fvideoplayback%253Fitag%253D22%2526signature%253D930B962DC4371784376CC85173E5AFB9423D0B13.B355F7259274C2E5BDF498BEC4223B299FE984F1%2526mm%253D31%2526ms%253Dau%2526mv%253Dm%2526mt%253D1430040329%2526dur%253D648.835%2526sver%253D3%2526ratebypass%253Dyes%2526fexp%253D900504%25252C900720%25252C907263%25252C916656%25252C919157%25252C934954%25252C938028%25252C9407115%25252C9407432%25252C9407642%25252C9407739%25252C9408133%25252C9408347%25252C9408707%25252C9409060%25252C9409253%25252C9409265%25252C9412765%25252C947233%25252C948124%25252C948703%25252C948801%25252C951703%25252C952612%25252C952626%25252C952637%25252C957201%2526ipbits%253D0%2526mime%253Dvideo%25252Fmp4%2526nh%253DEAI%2526upn%253DImSfiff0rCk%2526initcwndbps%253D780000%2526source%253Dyoutube%2526sparams%253Ddur%25252Cid%25252Cinitcwndbps%25252Cip%25252Cipbits%25252Citag%25252Cmime%25252Cmm%25252Cms%25252Cmv%25252Cnh%25252Cpl%25252Cratebypass%25252Csource%25252Cupn%25252Cexpire%2526pl%253D26%2526key%253Dyt5%2526ip%253D91.52.136.87%2526expire%253D1430061972%2526id%253Do-ANFXU_PE4Fp9m3Ft3XKHt_G1fWrNCgY-NgYJvlu6_QB5%26type%3Dvideo%252Fmp4%253B%2Bcodecs%253D%2522avc1.64001F%252C%2Bmp4a.40.2%2522%26quality%3Dhd720%26fallback_host%3Dtc.v11.cache8.googlevideo.com%2Citag%3D43%26url%3Dhttp%253A%252F%252Fr8---sn-5hn7ym7s.googlevideo.com%252Fvideoplayback%253Fitag%253D43%2526signature%253DD3EC151B0A4105F67A131F510442D06B84648030.4BDCBF2E872EE788A5C785A7374220FFA2BB15B2%2526mm%253D31%2526ms%253Dau%2526mv%253Dm%2526mt%253D1430040329%2526dur%253D0.000%2526sver%253D3%2526ratebypass%253Dyes%2526fexp%253D900504%25252C900720%25252C907263%25252C916656%25252C919157%25252C934954%25252C938028%25252C9407115%25252C9407432%25252C9407642%25252C9407739%25252C9408133%25252C9408347%25252C9408707%25252C9409060%25252C9409253%25252C9409265%25252C9412765%25252C947233%25252C948124%25252C948703%25252C948801%25252C951703%25252C952612%25252C952626%25252C952637%25252C957201%2526ipbits%253D0%2526mime%253Dvideo%25252Fwebm%2526nh%253DEAI%2526upn%253DImSfiff0rCk%2526initcwndbps%253D780000%2526source%253Dyoutube%2526sparams%253Ddur%25252Cid%25252Cinitcwndbps%25252Cip%25252Cipbits%25252Citag%25252Cmime%25252Cmm%25252Cms%25252Cmv%25252Cnh%25252Cpl%25252Cratebypass%25252Csource%25252Cupn%25252Cexpire%2526pl%253D26%2526key%253Dyt5%2526ip%253D91.52.136.87%2526expire%253D1430061972%2526id%253Do-ANFXU_PE4Fp9m3Ft3XKHt_G1fWrNCgY-NgYJvlu6_QB5%26type%3Dvideo%252Fwebm%253B%2Bcodecs%253D%2522vp8.0%252C%2Bvorbis%2522%26quality%3Dmedium%26fallback_host%3Dtc.v16.cache1.googlevideo.com%2Citag%3D18%26url%3Dhttp%253A%252F%252Fr8---sn-5hn7ym7s.googlevideo.com%252Fvideoplayback%253Fitag%253D18%2526signature%253D72A735FB705458A7C9FDF7FAF15EC752440D0620.6C7CD748FDCC1EF2394F84A07BAFB9C2A67D07FA%2526mm%253D31%2526ms%253Dau%2526mv%253Dm%2526mt%253D1430040329%2526dur%253D648.835%2526sver%253D3%2526ratebypass%253Dyes%2526fexp%253D900504%25252C900720%25252C907263%25252C916656%25252C919157%25252C934954%25252C938028%25252C9407115%25252C9407432%25252C9407642%25252C9407739%25252C9408133%25252C9408347%25252C9408707%25252C9409060%25252C9409253%25252C9409265%25252C9412765%25252C947233%25252C948124%25252C948703%25252C948801%25252C951703%25252C952612%25252C952626%25252C952637%25252C957201%2526ipbits%253D0%2526mime%253Dvideo%25252Fmp4%2526nh%253DEAI%2526upn%253DImSfiff0rCk%2526initcwndbps%253D780000%2526source%253Dyoutube%2526sparams%253Ddur%25252Cid%25252Cinitcwndbps%25252Cip%25252Cipbits%25252Citag%25252Cmime%25252Cmm%25252Cms%25252Cmv%25252Cnh%25252Cpl%25252Cratebypass%25252Csource%25252Cupn%25252Cexpire%2526pl%253D26%2526key%253Dyt5%2526ip%253D91.52.136.87%2526expire%253D1430061972%2526id%253Do-ANFXU_PE4Fp9m3Ft3XKHt_G1fWrNCgY-NgYJvlu6_QB5%26type%3Dvideo%252Fmp4%253B%2Bcodecs%253D%2522avc1.42001E%252C%2Bmp4a.40.2%2522%26quality%3Dmedium%26fallback_host%3Dtc.v7.cache3.googlevideo.com%2Citag%3D5%26url%3Dhttp%253A%252F%252Fr8---sn-5hn7ym7s.googlevideo.com%252Fvideoplayback%253Fmime%253Dvideo%25252Fx-flv%2526nh%253DEAI%2526signature%253D567A89A16F59028A2AC45889900BA38040EB273C.28D6D3D946F0CCDF95AE2E4AFB310F46D492635A%2526mt%253D1430040329%2526itag%253D5%2526upn%253DImSfiff0rCk%2526ip%253D91.52.136.87%2526mm%253D31%2526ms%253Dau%2526ipbits%253D0%2526mv%253Dm%2526initcwndbps%253D780000%2526source%253Dyoutube%2526sparams%253Ddur%25252Cid%25252Cinitcwndbps%25252Cip%25252Cipbits%25252Citag%25252Cmime%25252Cmm%25252Cms%25252Cmv%25252Cnh%25252Cpl%25252Csource%25252Cupn%25252Cexpire%2526expire%253D1430061972%2526pl%253D26%2526id%253Do-ANFXU_PE4Fp9m3Ft3XKHt_G1fWrNCgY-NgYJvlu6_QB5%2526dur%253D648.803%2526key%253Dyt5%2526sver%253D3%2526fexp%253D900504%25252C900720%25252C907263%25252C916656%25252C919157%25252C934954%25252C938028%25252C9407115%25252C9407432%25252C9407642%25252C9407739%25252C9408133%25252C9408347%25252C9408707%25252C9409060%25252C9409253%25252C9409265%25252C9412765%25252C947233%25252C948124%25252C948703%25252C948801%25252C951703%25252C952612%25252C952626%25252C952637%25252C957201%26type%3Dvideo%252Fx-flv%26quality%3Dsmall%26fallback_host%3Dtc.v19.cache3.googlevideo.com%2Citag%3D36%26url%3Dhttp%253A%252F%252Fr8---sn-5hn7ym7s.googlevideo.com%252Fvideoplayback%253Fmime%253Dvideo%25252F3gpp%2526nh%253DEAI%2526signature%253DF5990B0AB0563A16340E8B3B418C37DB95412CB1.706AD4DD1D6143F5BFB627505705BFCE089450AB%2526mt%253D1430040329%2526itag%253D36%2526upn%253DImSfiff0rCk%2526ip%253D91.52.136.87%2526mm%253D31%2526ms%253Dau%2526ipbits%253D0%2526mv%253Dm%2526initcwndbps%253D780000%2526source%253Dyoutube%2526sparams%253Ddur%25252Cid%25252Cinitcwndbps%25252Cip%25252Cipbits%25252Citag%25252Cmime%25252Cmm%25252Cms%25252Cmv%25252Cnh%25252Cpl%25252Csource%25252Cupn%25252Cexpire%2526expire%253D1430061972%2526pl%253D26%2526id%253Do-ANFXU_PE4Fp9m3Ft3XKHt_G1fWrNCgY-NgYJvlu6_QB5%2526dur%253D648.951%2526key%253Dyt5%2526sver%253D3%2526fexp%253D900504%25252C900720%25252C907263%25252C916656%25252C919157%25252C934954%25252C938028%25252C9407115%25252C9407432%25252C9407642%25252C9407739%25252C9408133%25252C9408347%25252C9408707%25252C9409060%25252C9409253%25252C9409265%25252C9412765%25252C947233%25252C948124%25252C948703%25252C948801%25252C951703%25252C952612%25252C952626%25252C952637%25252C957201%26type%3Dvideo%252F3gpp%253B%2Bcodecs%253D%2522mp4v.20.3%252C%2Bmp4a.40.2%2522%26quality%3Dsmall%26fallback_host%3Dtc.v18.cache2.googlevideo.com%2Citag%3D17%26url%3Dhttp%253A%252F%252Fr8---sn-5hn7ym7s.googlevideo.com%252Fvideoplayback%253Fmime%253Dvideo%25252F3gpp%2526nh%253DEAI%2526signature%253D3848E66BD11D129857E4F885ECAC318161313D0F.CD6717AB920D034FCDFA8B99B3BB766D7A46D053%2526mt%253D1430040329%2526itag%253D17%2526upn%253DImSfiff0rCk%2526ip%253D91.52.136.87%2526mm%253D31%2526ms%253Dau%2526ipbits%253D0%2526mv%253Dm%2526initcwndbps%253D780000%2526source%253Dyoutube%2526sparams%253Ddur%25252Cid%25252Cinitcwndbps%25252Cip%25252Cipbits%25252Citag%25252Cmime%25252Cmm%25252Cms%25252Cmv%25252Cnh%25252Cpl%25252Csource%25252Cupn%25252Cexpire%2526expire%253D1430061972%2526pl%253D26%2526id%253Do-ANFXU_PE4Fp9m3Ft3XKHt_G1fWrNCgY-NgYJvlu6_QB5%2526dur%253D649.137%2526key%253Dyt5%2526sver%253D3%2526fexp%253D900504%25252C900720%25252C907263%25252C916656%25252C919157%25252C934954%25252C938028%25252C9407115%25252C9407432%25252C9407642%25252C9407739%25252C9408133%25252C9408347%25252C9408707%25252C9409060%25252C9409253%25252C9409265%25252C9412765%25252C947233%25252C948124%25252C948703%25252C948801%25252C951703%25252C952612%25252C952626%25252C952637%25252C957201%26type%3Dvideo%252F3gpp%253B%2Bcodecs%253D%2522mp4v.20.3%252C%2Bmp4a.40.2%2522%26quality%3Dsmall%26fallback_host%3Dtc.v11.cache7.googlevideo.com&eventid=NK88VfOnCOmSjAaS9IDgDw&cc_font=Arial+Unicode+MS%2C+arial%2C+verdana%2C+_sans&of=6H2iVPGiTHr296GVFuNbYA&author=Twitter+University&default_audio_track_index=0&cc_fonts_url=http%3A%2F%2Fs.ytimg.com%2Fyts%2Fswfbin%2Fplayer-vflgoXED9%2Ffonts708.swf&iurlsd=http%3A%2F%2Fi.ytimg.com%2Fvi%2Fpts6F00GFuU%2Fsddefault.jpg&cc_module=http%3A%2F%2Fs.ytimg.com%2Fyts%2Fswfbin%2Fplayer-vflgoXED9%2Fsubtitle_module.swf&caption_translation_languages=lc%3Daf%26n%3DAfrikaans%2Clc%3Dsq%26n%3DAlbanisch%2Clc%3Dar%26n%3DArabisch%2Clc%3Dhy%26n%3DArmenisch%2Clc%3Daz%26n%3DAserbaidschanisch%2Clc%3Deu%26n%3DBaskisch%2Clc%3Dbn%26n%3DBengalisch%2Clc%3Dmy%26n%3DBirmanisch%2Clc%3Dbs%26n%3DBosnisch%2Clc%3Dbg%26n%3DBulgarisch%2Clc%3Dceb%26n%3DCebuano%2Clc%3Dzh-Hant%26n%3DChinesisch%2B%2528Traditionell%2529%2Clc%3Dzh-Hans%26n%3DChinesisch%2B%2528Vereinfacht%2529%2Clc%3Dda%26n%3DD%25C3%25A4nisch%2Clc%3Dde%26n%3DDeutsch%2Clc%3Den%26n%3DEnglisch%2Clc%3Deo%26n%3DEsperanto%2Clc%3Det%26n%3DEstnisch%2Clc%3Dfil%26n%3DFilipino%2Clc%3Dfi%26n%3DFinnisch%2Clc%3Dfr%26n%3DFranz%25C3%25B6sisch%2Clc%3Dgl%26n%3DGalizisch%2Clc%3Dka%26n%3DGeorgisch%2Clc%3Del%26n%3DGriechisch%2Clc%3Dgu%26n%3DGujarati%2Clc%3Dht%26n%3DHaitianisch%2Clc%3Dha%26n%3DHausa%2Clc%3Diw%26n%3DHebr%25C3%25A4isch%2Clc%3Dhi%26n%3DHindi%2Clc%3Dig%26n%3DIgbo%2Clc%3Did%26n%3DIndonesisch%2Clc%3Dga%26n%3DIrisch%2Clc%3Dis%26n%3DIsl%25C3%25A4ndisch%2Clc%3Dit%26n%3DItalienisch%2Clc%3Dja%26n%3DJapanisch%2Clc%3Djv%26n%3DJavanisch%2Clc%3Dyi%26n%3DJiddisch%2Clc%3Dkm%26n%3DKambodschanisch%2Clc%3Dkn%26n%3DKannada%2Clc%3Dkk%26n%3DKasachisch%2Clc%3Dca%26n%3DKatalanisch%2Clc%3Dko%26n%3DKoreanisch%2Clc%3Dhr%26n%3DKroatisch%2Clc%3Dlo%26n%3DLaotisch%2Clc%3Dla%26n%3DLatein%2Clc%3Dlv%26n%3DLettisch%2Clc%3Dlt%26n%3DLitauisch%2Clc%3Dmg%26n%3DMadagassisch%2Clc%3Dms%26n%3DMalaiisch%2Clc%3Dml%26n%3DMalayalam%2Clc%3Dmt%26n%3DMaltesisch%2Clc%3Dmi%26n%3DMaori%2Clc%3Dmr%26n%3DMarathi%2Clc%3Dmk%26n%3DMazedonisch%2Clc%3Dhmn%26n%3DMiao-Sprache%2Clc%3Dmn%26n%3DMongolisch%2Clc%3Dne%26n%3DNepalesisch%2Clc%3Dnl%26n%3DNiederl%25C3%25A4ndisch%2Clc%3Dno%26n%3DNorwegisch%2Clc%3Dny%26n%3DNyanja-Sprache%2Clc%3Dpa%26n%3DPanjabi%2Clc%3Dfa%26n%3DPersisch%2Clc%3Dpl%26n%3DPolnisch%2Clc%3Dpt%26n%3DPortugiesisch%2Clc%3Dro%26n%3DRum%25C3%25A4nisch%2Clc%3Dru%26n%3DRussisch%2Clc%3Dsv%26n%3DSchwedisch%2Clc%3Dsr%26n%3DSerbisch%2Clc%3Dsi%26n%3DSinghalesisch%2Clc%3Dsk%26n%3DSlowakisch%2Clc%3Dsl%26n%3DSlowenisch%2Clc%3Dso%26n%3DSomali%2Clc%3Des%26n%3DSpanisch%2Clc%3Dsw%26n%3DSuaheli%2Clc%3Dst%26n%3DS%25C3%25BCd-Sotho-Sprache%2Clc%3Dsu%26n%3DSundanesisch%2Clc%3Dtg%26n%3DTadschikisch%2Clc%3Dta%26n%3DTamilisch%2Clc%3Dte%26n%3DTelugu%2Clc%3Dth%26n%3DThail%25C3%25A4ndisch%2Clc%3Dcs%26n%3DTschechisch%2Clc%3Dtr%26n%3DT%25C3%25BCrkisch%2Clc%3Duk%26n%3DUkrainisch%2Clc%3Dhu%26n%3DUngarisch%2Clc%3Dur%26n%3DUrdu%2Clc%3Duz%26n%3DUsbekisch%2Clc%3Dvi%26n%3DVietnamesisch%2Clc%3Dcy%26n%3DWalisisch%2Clc%3Dbe%26n%3DWei%25C3%259Frussisch%2Clc%3Dyo%26n%3DYoruba%2Clc%3Dzu%26n%3DZulu&tmi=1&avg_rating=4.96536796537&allow_embed=1&ttsurl=http%3A%2F%2Fwww.youtube.com%2Fapi%2Ftimedtext%3Fsparams%3Dasr_langs%252Ccaps%252Cv%252Cexpire%26expire%3D1430065572%26hl%3Dde_DE%26asr_langs%3Dde%252Cen%252Cko%252Cru%252Cpt%252Cja%252Cit%252Ces%252Cnl%252Cfr%26signature%3D379A46C189B0BFF4157662E5A314DC349F82D159.7878262566FA6DAB03A3DBA0312B6D828E297465%26key%3Dyttt1%26caps%3Dasr%26v%3Dpts6F00GFuU&thumbnail_url=http%3A%2F%2Fi.ytimg.com%2Fvi%2Fpts6F00GFuU%2Fdefault.jpg&length_seconds=649&dashmpd=http%3A%2F%2Fmanifest.googlevideo.com%2Fapi%2Fmanifest%2Fdash%2Fnh%2FEAI%2Fplayback_host%2Fr8---sn-5hn7ym7s.googlevideo.com%2Fsignature%2F6A0523A14EE5C6521CA17B32CFE29EDA09B40446.CD04469A9C39D6772389F249FD095971059F4475%2Fmt%2F1430040329%2Fitag%2F0%2Fupn%2FbDW0SmIa_Vk%2Fip%2F91.52.136.87%2Fmm%2F31%2Fms%2Fau%2Fipbits%2F0%2Fmv%2Fm%2Fas%2Ffmp4_audio_clear%252Cwebm_audio_clear%252Cfmp4_sd_hd_clear%252Cwebm_sd_hd_clear%252Cwebm2_sd_hd_clear%2Fsource%2Fyoutube%2Fsparams%2Fas%252Cid%252Cip%252Cipbits%252Citag%252Cmm%252Cms%252Cmv%252Cnh%252Cpl%252Cplayback_host%252Csource%252Cexpire%2Fexpire%2F1430061972%2Fpl%2F26%2Fid%2Fo-ANFXU_PE4Fp9m3Ft3XKHt_G1fWrNCgY-NgYJvlu6_QB5%2Fkey%2Fyt5%2Fsver%2F3%2Ffexp%2F900504%252C900720%252C907263%252C916656%252C919157%252C934954%252C938028%252C9407115%252C9407432%252C9407642%252C9407739%252C9408133%252C9408347%252C9408707%252C9409060%252C9409253%252C9409265%252C9412765%252C947233%252C948124%252C948703%252C948801%252C951703%252C952612%252C952626%252C952637%252C957201&fmt_list=22%2F1280x720%2F9%2F0%2F115%2C43%2F640x360%2F99%2F0%2F0%2C18%2F640x360%2F9%2F0%2F115%2C5%2F426x240%2F7%2F0%2F0%2C36%2F426x240%2F99%2F1%2F0%2C17%2F256x144%2F99%2F1%2F0&iurlmq=http%3A%2F%2Fi.ytimg.com%2Fvi%2Fpts6F00GFuU%2Fmqdefault.jpg&fexp=900504%2C900720%2C907263%2C916656%2C919157%2C934954%2C938028%2C9407115%2C9407432%2C9407642%2C9407739%2C9408133%2C9408347%2C9408707%2C9409060%2C9409253%2C9409265%2C9412765%2C947233%2C948124%2C948703%2C948801%2C951703%2C952612%2C952626%2C952637%2C957201&iurl=http%3A%2F%2Fi.ytimg.com%2Fvi%2Fpts6F00GFuU%2Fhqdefault.jpg&muted=0&plid=AAUUnThlujWoyXY4&ptk=youtube_none&caption_audio_tracks=v%3D0%26i%3D0&ldpj=-28&csi_page_type=embed&pltype=contentugc&c=WEB&storyboard_spec=http%3A%2F%2Fi.ytimg.com%2Fsb%2Fpts6F00GFuU%2Fstoryboard3_L%24L%2F%24N.jpg%7C48%2327%23100%2310%2310%230%23default%23tbZAfHHOgf4Qe1K8BuJCW7NKz1Y%7C80%2345%23131%2310%2310%235000%23M%24M%23_NnElR_DIf8IofKsDQM4xXTSVcM%7C160%2390%23131%235%235%235000%23M%24M%23-Z20w7P9MG3YrKpGCxfDRiayj0U&enablecsi=1&hl=de_DE&title=Docker+at+Spotify&cc_asr=1&use_cipher_signature=False&status=ok&video_id=pts6F00GFuU&iurlmaxres=http%3A%2F%2Fi.ytimg.com%2Fvi%2Fpts6F00GFuU%2Fmaxresdefault.jpg&loudness=-21.7089996338&adaptive_fmts=url%3Dhttp%253A%252F%252Fr8---sn-5hn7ym7s.googlevideo.com%252Fvideoplayback%253Fgir%253Dyes%2526itag%253D136%2526signature%253DBE729C4454AAFA85DA0B95867B509526C46AB1F0.D4B77697B1186B7E54098BEAA4CF554FDBE6017A%2526mm%253D31%2526ms%253Dau%2526mv%253Dm%2526mt%253D1430040329%2526dur%253D648.731%2526sver%253D3%2526fexp%253D900504%25252C900720%25252C907263%25252C916656%25252C919157%25252C934954%25252C938028%25252C9407115%25252C9407432%25252C9407642%25252C9407739%25252C9408133%25252C9408347%25252C9408707%25252C9409060%25252C9409253%25252C9409265%25252C9412765%25252C947233%25252C948124%25252C948703%25252C948801%25252C951703%25252C952612%25252C952626%25252C952637%25252C957201%2526ipbits%253D0%2526mime%253Dvideo%25252Fmp4%2526clen%253D57481354%2526nh%253DEAI%2526upn%253DgDjkn3gtCuM%2526initcwndbps%253D780000%2526source%253Dyoutube%2526sparams%253Dclen%25252Cdur%25252Cgir%25252Cid%25252Cinitcwndbps%25252Cip%25252Cipbits%25252Citag%25252Clmt%25252Cmime%25252Cmm%25252Cms%25252Cmv%25252Cnh%25252Cpl%25252Csource%25252Cupn%25252Cexpire%2526pl%253D26%2526key%253Dyt5%2526lmt%253D1386971067470147%2526ip%253D91.52.136.87%2526expire%253D1430061972%2526id%253Do-ANFXU_PE4Fp9m3Ft3XKHt_G1fWrNCgY-NgYJvlu6_QB5%26type%3Dvideo%252Fmp4%253B%2Bcodecs%253D%2522avc1.4d401f%2522%26itag%3D136%26size%3D1280x720%26init%3D0-707%26bitrate%3D1748135%26fps%3D24%26index%3D708-2299%26lmt%3D1386971067470147%26projection_type%3D1%26clen%3D57481354%2Curl%3Dhttp%253A%252F%252Fr8---sn-5hn7ym7s.googlevideo.com%252Fvideoplayback%253Fgir%253Dyes%2526itag%253D247%2526signature%253D0B99F259490EA51C66BB9DA27B00EDF188EB50AC.56A23F95EB338B08862E2ADBC14DFDE2347DC681%2526mm%253D31%2526ms%253Dau%2526mv%253Dm%2526mt%253D1430040329%2526dur%253D648.731%2526sver%253D3%2526fexp%253D900504%25252C900720%25252C907263%25252C916656%25252C919157%25252C934954%25252C938028%25252C9407115%25252C9407432%25252C9407642%25252C9407739%25252C9408133%25252C9408347%25252C9408707%25252C9409060%25252C9409253%25252C9409265%25252C9412765%25252C947233%25252C948124%25252C948703%25252C948801%25252C951703%25252C952612%25252C952626%25252C952637%25252C957201%2526ipbits%253D0%2526mime%253Dvideo%25252Fwebm%2526clen%253D31921036%2526nh%253DEAI%2526upn%253DgDjkn3gtCuM%2526initcwndbps%253D780000%2526source%253Dyoutube%2526sparams%253Dclen%25252Cdur%25252Cgir%25252Cid%25252Cinitcwndbps%25252Cip%25252Cipbits%25252Citag%25252Clmt%25252Cmime%25252Cmm%25252Cms%25252Cmv%25252Cnh%25252Cpl%25252Csource%25252Cupn%25252Cexpire%2526pl%253D26%2526key%253Dyt5%2526lmt%253D1400216214481054%2526ip%253D91.52.136.87%2526expire%253D1430061972%2526id%253Do-ANFXU_PE4Fp9m3Ft3XKHt_G1fWrNCgY-NgYJvlu6_QB5%26type%3Dvideo%252Fwebm%253B%2Bcodecs%253D%2522vp9%2522%26itag%3D247%26size%3D1280x720%26init%3D0-234%26bitrate%3D1221816%26fps%3D1%26index%3D235-2458%26lmt%3D1400216214481054%26projection_type%3D1%26clen%3D31921036%2Curl%3Dhttp%253A%252F%252Fr8---sn-5hn7ym7s.googlevideo.com%252Fvideoplayback%253Fgir%253Dyes%2526itag%253D135%2526signature%253D6666164D058E528B941CE41F0CE3475EE34F2C0D.0A904A1EDD59244989F3AF87E02FFBF62DCE8CA9%2526mm%253D31%2526ms%253Dau%2526mv%253Dm%2526mt%253D1430040329%2526dur%253D648.731%2526sver%253D3%2526fexp%253D900504%25252C900720%25252C907263%25252C916656%25252C919157%25252C934954%25252C938028%25252C9407115%25252C9407432%25252C9407642%25252C9407739%25252C9408133%25252C9408347%25252C9408707%25252C9409060%25252C9409253%25252C9409265%25252C9412765%25252C947233%25252C948124%25252C948703%25252C948801%25252C951703%25252C952612%25252C952626%25252C952637%25252C957201%2526ipbits%253D0%2526mime%253Dvideo%25252Fmp4%2526clen%253D28733013%2526nh%253DEAI%2526upn%253DgDjkn3gtCuM%2526initcwndbps%253D780000%2526source%253Dyoutube%2526sparams%253Dclen%25252Cdur%25252Cgir%25252Cid%25252Cinitcwndbps%25252Cip%25252Cipbits%25252Citag%25252Clmt%25252Cmime%25252Cmm%25252Cms%25252Cmv%25252Cnh%25252Cpl%25252Csource%25252Cupn%25252Cexpire%2526pl%253D26%2526key%253Dyt5%2526lmt%253D1386971067469166%2526ip%253D91.52.136.87%2526expire%253D1430061972%2526id%253Do-ANFXU_PE4Fp9m3Ft3XKHt_G1fWrNCgY-NgYJvlu6_QB5%26type%3Dvideo%252Fmp4%253B%2Bcodecs%253D%2522avc1.4d401e%2522%26itag%3D135%26size%3D854x480%26init%3D0-707%26bitrate%3D802528%26fps%3D24%26index%3D708-2299%26lmt%3D1386971067469166%26projection_type%3D1%26clen%3D28733013%2Curl%3Dhttp%253A%252F%252Fr8---sn-5hn7ym7s.googlevideo.com%252Fvideoplayback%253Fgir%253Dyes%2526itag%253D244%2526signature%253D4AD5BB925040744BE4983E401C0B87DAA87112FA.764CFD2F4945E6C2E5B23519F4B1F68D9FD00F08%2526mm%253D31%2526ms%253Dau%2526mv%253Dm%2526mt%253D1430040329%2526dur%253D648.731%2526sver%253D3%2526fexp%253D900504%25252C900720%25252C907263%25252C916656%25252C919157%25252C934954%25252C938028%25252C9407115%25252C9407432%25252C9407642%25252C9407739%25252C9408133%25252C9408347%25252C9408707%25252C9409060%25252C9409253%25252C9409265%25252C9412765%25252C947233%25252C948124%25252C948703%25252C948801%25252C951703%25252C952612%25252C952626%25252C952637%25252C957201%2526ipbits%253D0%2526mime%253Dvideo%25252Fwebm%2526clen%253D15768933%2526nh%253DEAI%2526upn%253DgDjkn3gtCuM%2526initcwndbps%253D780000%2526source%253Dyoutube%2526sparams%253Dclen%25252Cdur%25252Cgir%25252Cid%25252Cinitcwndbps%25252Cip%25252Cipbits%25252Citag%25252Clmt%25252Cmime%25252Cmm%25252Cms%25252Cmv%25252Cnh%25252Cpl%25252Csource%25252Cupn%25252Cexpire%2526pl%253D26%2526key%253Dyt5%2526lmt%253D1400214837000359%2526ip%253D91.52.136.87%2526expire%253D1430061972%2526id%253Do-ANFXU_PE4Fp9m3Ft3XKHt_G1fWrNCgY-NgYJvlu6_QB5%26type%3Dvideo%252Fwebm%253B%2Bcodecs%253D%2522vp9%2522%26itag%3D244%26size%3D854x480%26init%3D0-234%26bitrate%3D594226%26fps%3D1%26index%3D235-2433%26lmt%3D1400214837000359%26projection_type%3D1%26clen%3D15768933%2Curl%3Dhttp%253A%252F%252Fr8---sn-5hn7ym7s.googlevideo.com%252Fvideoplayback%253Fgir%253Dyes%2526itag%253D134%2526signature%253D10E04BB4D245A91BE0BB09B20D9B657B3E772F6A.5C91F1A96956DB10FA5F3894556B3493C509C0AC%2526mm%253D31%2526ms%253Dau%2526mv%253Dm%2526mt%253D1430040329%2526dur%253D648.731%2526sver%253D3%2526fexp%253D900504%25252C900720%25252C907263%25252C916656%25252C919157%25252C934954%25252C938028%25252C9407115%25252C9407432%25252C9407642%25252C9407739%25252C9408133%25252C9408347%25252C9408707%25252C9409060%25252C9409253%25252C9409265%25252C9412765%25252C947233%25252C948124%25252C948703%25252C948801%25252C951703%25252C952612%25252C952626%25252C952637%25252C957201%2526ipbits%253D0%2526mime%253Dvideo%25252Fmp4%2526clen%253D14037521%2526nh%253DEAI%2526upn%253DgDjkn3gtCuM%2526initcwndbps%253D780000%2526source%253Dyoutube%2526sparams%253Dclen%25252Cdur%25252Cgir%25252Cid%25252Cinitcwndbps%25252Cip%25252Cipbits%25252Citag%25252Clmt%25252Cmime%25252Cmm%25252Cms%25252Cmv%25252Cnh%25252Cpl%25252Csource%25252Cupn%25252Cexpire%2526pl%253D26%2526key%253Dyt5%2526lmt%253D1386971063044674%2526ip%253D91.52.136.87%2526expire%253D1430061972%2526id%253Do-ANFXU_PE4Fp9m3Ft3XKHt_G1fWrNCgY-NgYJvlu6_QB5%26type%3Dvideo%252Fmp4%253B%2Bcodecs%253D%2522avc1.4d401e%2522%26itag%3D134%26size%3D640x360%26init%3D0-707%26bitrate%3D407101%26fps%3D24%26index%3D708-2299%26lmt%3D1386971063044674%26projection_type%3D1%26clen%3D14037521%2Curl%3Dhttp%253A%252F%252Fr8---sn-5hn7ym7s.googlevideo.com%252Fvideoplayback%253Fgir%253Dyes%2526itag%253D243%2526signature%253D70CC1C4A79C055006FCFD9304DC55251B2DC6DA7.34B28146809D16594BEFD66B05B93D9DCC179AAB%2526mm%253D31%2526ms%253Dau%2526mv%253Dm%2526mt%253D1430040329%2526dur%253D648.731%2526sver%253D3%2526fexp%253D900504%25252C900720%25252C907263%25252C916656%25252C919157%25252C934954%25252C938028%25252C9407115%25252C9407432%25252C9407642%25252C9407739%25252C9408133%25252C9408347%25252C9408707%25252C9409060%25252C9409253%25252C9409265%25252C9412765%25252C947233%25252C948124%25252C948703%25252C948801%25252C951703%25252C952612%25252C952626%25252C952637%25252C957201%2526ipbits%253D0%2526mime%253Dvideo%25252Fwebm%2526clen%253D8713220%2526nh%253DEAI%2526upn%253DgDjkn3gtCuM%2526initcwndbps%253D780000%2526source%253Dyoutube%2526sparams%253Dclen%25252Cdur%25252Cgir%25252Cid%25252Cinitcwndbps%25252Cip%25252Cipbits%25252Citag%25252Clmt%25252Cmime%25252Cmm%25252Cms%25252Cmv%25252Cnh%25252Cpl%25252Csource%25252Cupn%25252Cexpire%2526pl%253D26%2526key%253Dyt5%2526lmt%253D1400214688726626%2526ip%253D91.52.136.87%2526expire%253D1430061972%2526id%253Do-ANFXU_PE4Fp9m3Ft3XKHt_G1fWrNCgY-NgYJvlu6_QB5%26type%3Dvideo%252Fwebm%253B%2Bcodecs%253D%2522vp9%2522%26itag%3D243%26size%3D640x360%26init%3D0-234%26bitrate%3D305384%26fps%3D1%26index%3D235-2433%26lmt%3D1400214688726626%26projection_type%3D1%26clen%3D8713220%2Curl%3Dhttp%253A%252F%252Fr8---sn-5hn7ym7s.googlevideo.com%252Fvideoplayback%253Fgir%253Dyes%2526itag%253D133%2526signature%253D4F925259AAC100E085455D22EC1A51CB24494C82.81F904B000B17AFCFE34F1E33D5CBEEDE1CB9791%2526mm%253D31%2526ms%253Dau%2526mv%253Dm%2526mt%253D1430040329%2526dur%253D648.731%2526sver%253D3%2526fexp%253D900504%25252C900720%25252C907263%25252C916656%25252C919157%25252C934954%25252C938028%25252C9407115%25252C9407432%25252C9407642%25252C9407739%25252C9408133%25252C9408347%25252C9408707%25252C9409060%25252C9409253%25252C9409265%25252C9412765%25252C947233%25252C948124%25252C948703%25252C948801%25252C951703%25252C952612%25252C952626%25252C952637%25252C957201%2526ipbits%253D0%2526mime%253Dvideo%25252Fmp4%2526clen%253D11129410%2526nh%253DEAI%2526upn%253DgDjkn3gtCuM%2526initcwndbps%253D780000%2526source%253Dyoutube%2526sparams%253Dclen%25252Cdur%25252Cgir%25252Cid%25252Cinitcwndbps%25252Cip%25252Cipbits%25252Citag%25252Clmt%25252Cmime%25252Cmm%25252Cms%25252Cmv%25252Cnh%25252Cpl%25252Csource%25252Cupn%25252Cexpire%2526pl%253D26%2526key%253Dyt5%2526lmt%253D1386971062043764%2526ip%253D91.52.136.87%2526expire%253D1430061972%2526id%253Do-ANFXU_PE4Fp9m3Ft3XKHt_G1fWrNCgY-NgYJvlu6_QB5%26type%3Dvideo%252Fmp4%253B%2Bcodecs%253D%2522avc1.4d4015%2522%26itag%3D133%26size%3D426x240%26init%3D0-671%26bitrate%3D283284%26fps%3D24%26index%3D672-2263%26lmt%3D1386971062043764%26projection_type%3D1%26clen%3D11129410%2Curl%3Dhttp%253A%252F%252Fr8---sn-5hn7ym7s.googlevideo.com%252Fvideoplayback%253Fgir%253Dyes%2526itag%253D242%2526signature%253DDFD2C968C38139A75B8F26CF4E5B2A56C9E6B0C5.935B5548718701D0D58486AC02A8EBA0F5CFEAA7%2526mm%253D31%2526ms%253Dau%2526mv%253Dm%2526mt%253D1430040329%2526dur%253D648.731%2526sver%253D3%2526fexp%253D900504%25252C900720%25252C907263%25252C916656%25252C919157%25252C934954%25252C938028%25252C9407115%25252C9407432%25252C9407642%25252C9407739%25252C9408133%25252C9408347%25252C9408707%25252C9409060%25252C9409253%25252C9409265%25252C9412765%25252C947233%25252C948124%25252C948703%25252C948801%25252C951703%25252C952612%25252C952626%25252C952637%25252C957201%2526ipbits%253D0%2526mime%253Dvideo%25252Fwebm%2526clen%253D4562024%2526nh%253DEAI%2526upn%253DgDjkn3gtCuM%2526initcwndbps%253D780000%2526source%253Dyoutube%2526sparams%253Dclen%25252Cdur%25252Cgir%25252Cid%25252Cinitcwndbps%25252Cip%25252Cipbits%25252Citag%25252Clmt%25252Cmime%25252Cmm%25252Cms%25252Cmv%25252Cnh%25252Cpl%25252Csource%25252Cupn%25252Cexpire%2526pl%253D26%2526key%253Dyt5%2526lmt%253D1400214645372917%2526ip%253D91.52.136.87%2526expire%253D1430061972%2526id%253Do-ANFXU_PE4Fp9m3Ft3XKHt_G1fWrNCgY-NgYJvlu6_QB5%26type%3Dvideo%252Fwebm%253B%2Bcodecs%253D%2522vp9%2522%26itag%3D242%26size%3D426x240%26init%3D0-233%26bitrate%3D156330%26fps%3D1%26index%3D234-2431%26lmt%3D1400214645372917%26projection_type%3D1%26clen%3D4562024%2Curl%3Dhttp%253A%252F%252Fr8---sn-5hn7ym7s.googlevideo.com%252Fvideoplayback%253Fgir%253Dyes%2526itag%253D160%2526signature%253D1E90667A194EC7E1536DB71703D692F640625B3E.50E13F470211515DA9E9AF7047F787ABE31ABB2B%2526mm%253D31%2526ms%253Dau%2526mv%253Dm%2526mt%253D1430040329%2526dur%253D648.731%2526sver%253D3%2526fexp%253D900504%25252C900720%25252C907263%25252C916656%25252C919157%25252C934954%25252C938028%25252C9407115%25252C9407432%25252C9407642%25252C9407739%25252C9408133%25252C9408347%25252C9408707%25252C9409060%25252C9409253%25252C9409265%25252C9412765%25252C947233%25252C948124%25252C948703%25252C948801%25252C951703%25252C952612%25252C952626%25252C952637%25252C957201%2526ipbits%253D0%2526mime%253Dvideo%25252Fmp4%2526clen%253D5479110%2526nh%253DEAI%2526upn%253DgDjkn3gtCuM%2526initcwndbps%253D780000%2526source%253Dyoutube%2526sparams%253Dclen%25252Cdur%25252Cgir%25252Cid%25252Cinitcwndbps%25252Cip%25252Cipbits%25252Citag%25252Clmt%25252Cmime%25252Cmm%25252Cms%25252Cmv%25252Cnh%25252Cpl%25252Csource%25252Cupn%25252Cexpire%2526pl%253D26%2526key%253Dyt5%2526lmt%253D1386971062043603%2526ip%253D91.52.136.87%2526expire%253D1430061972%2526id%253Do-ANFXU_PE4Fp9m3Ft3XKHt_G1fWrNCgY-NgYJvlu6_QB5%26type%3Dvideo%252Fmp4%253B%2Bcodecs%253D%2522avc1.4d400c%2522%26itag%3D160%26size%3D256x144%26init%3D0-670%26bitrate%3D123270%26fps%3D15%26index%3D671-2262%26lmt%3D1386971062043603%26projection_type%3D1%26clen%3D5479110%2Curl%3Dhttp%253A%252F%252Fr8---sn-5hn7ym7s.googlevideo.com%252Fvideoplayback%253Fgir%253Dyes%2526itag%253D140%2526signature%253D41F5449D5478CF6E13CE3F875632A147A780F07A.E95CF788F19AC252D526513806AAD662451B4AF4%2526mm%253D31%2526ms%253Dau%2526mv%253Dm%2526mt%253D1430040329%2526dur%253D648.835%2526sver%253D3%2526fexp%253D900504%25252C900720%25252C907263%25252C916656%25252C919157%25252C934954%25252C938028%25252C9407115%25252C9407432%25252C9407642%25252C9407739%25252C9408133%25252C9408347%25252C9408707%25252C9409060%25252C9409253%25252C9409265%25252C9412765%25252C947233%25252C948124%25252C948703%25252C948801%25252C951703%25252C952612%25252C952626%25252C952637%25252C957201%2526ipbits%253D0%2526mime%253Daudio%25252Fmp4%2526clen%253D10305629%2526nh%253DEAI%2526upn%253DgDjkn3gtCuM%2526initcwndbps%253D780000%2526source%253Dyoutube%2526sparams%253Dclen%25252Cdur%25252Cgir%25252Cid%25252Cinitcwndbps%25252Cip%25252Cipbits%25252Citag%25252Clmt%25252Cmime%25252Cmm%25252Cms%25252Cmv%25252Cnh%25252Cpl%25252Csource%25252Cupn%25252Cexpire%2526pl%253D26%2526key%253Dyt5%2526lmt%253D1386971063072923%2526ip%253D91.52.136.87%2526expire%253D1430061972%2526id%253Do-ANFXU_PE4Fp9m3Ft3XKHt_G1fWrNCgY-NgYJvlu6_QB5%26type%3Daudio%252Fmp4%253B%2Bcodecs%253D%2522mp4a.40.2%2522%26init%3D0-591%26projection_type%3D1%26itag%3D140%26bitrate%3D128374%26index%3D592-1403%26lmt%3D1386971063072923%26clen%3D10305629%2Curl%3Dhttp%253A%252F%252Fr8---sn-5hn7ym7s.googlevideo.com%252Fvideoplayback%253Fgir%253Dyes%2526itag%253D171%2526signature%253D665C7484FB81004CEDF1F0C78D8F36C7E2735520.27E62AFA7FD0B60E8737B1F73B59B829FEC64538%2526mm%253D31%2526ms%253Dau%2526mv%253Dm%2526mt%253D1430040329%2526dur%253D648.767%2526sver%253D3%2526fexp%253D900504%25252C900720%25252C907263%25252C916656%25252C919157%25252C934954%25252C938028%25252C9407115%25252C9407432%25252C9407642%25252C9407739%25252C9408133%25252C9408347%25252C9408707%25252C9409060%25252C9409253%25252C9409265%25252C9412765%25252C947233%25252C948124%25252C948703%25252C948801%25252C951703%25252C952612%25252C952626%25252C952637%25252C957201%2526ipbits%253D0%2526mime%253Daudio%25252Fwebm%2526clen%253D7273764%2526nh%253DEAI%2526upn%253DgDjkn3gtCuM%2526initcwndbps%253D780000%2526source%253Dyoutube%2526sparams%253Dclen%25252Cdur%25252Cgir%25252Cid%25252Cinitcwndbps%25252Cip%25252Cipbits%25252Citag%25252Clmt%25252Cmime%25252Cmm%25252Cms%25252Cmv%25252Cnh%25252Cpl%25252Csource%25252Cupn%25252Cexpire%2526pl%253D26%2526key%253Dyt5%2526lmt%253D1400206046750606%2526ip%253D91.52.136.87%2526expire%253D1430061972%2526id%253Do-ANFXU_PE4Fp9m3Ft3XKHt_G1fWrNCgY-NgYJvlu6_QB5%26type%3Daudio%252Fwebm%253B%2Bcodecs%253D%2522vorbis%2522%26init%3D0-4451%26projection_type%3D1%26itag%3D171%26bitrate%3D127941%26fps%3D1%26index%3D4452-5553%26lmt%3D1400206046750606%26clen%3D7273764&timestamp=1430040372&watermark=%2Chttp%3A%2F%2Fs.ytimg.com%2Fyts%2Fimg%2Fwatermark%2Fyoutube_watermark-vflHX6b6E.png%2Chttp%3A%2F%2Fs.ytimg.com%2Fyts%2Fimg%2Fwatermark%2Fyoutube_hd_watermark-vflAzLcD6.png&iurlhq=http%3A%2F%2Fi.ytimg.com%2Fvi%2Fpts6F00GFuU%2Fhqdefault.jpg&has_cc=True&idpj=-9&probe_url=http%3A%2F%2Fr3---sn-aigllm7s.googlevideo.com%2Fvideogoodput%3Fid%3Do-AMZbA8GxJviJOXnNEjijf5bYRHjxlu4esZGCSXC-s82B%26source%3Dgoodput%26range%3D0-4999%26expire%3D1430043972%26ip%3D91.52.136.87%26ms%3Dpm%26mm%3D35%26pl%3D26%26nh%3DEAQ%26sparams%3Did%2Csource%2Crange%2Cexpire%2Cip%2Cms%2Cmm%2Cpl%2Cnh%26signature%3D1735D3783C744129927A41A26DB285EC401247B9.4D89876FFDE0871CAA002FFD4C94EC5ED4463361%26key%3Dcms1&video_verticals=%5B3%5D&cl=92030301&cc3_module=1&view_count=42229&account_playback_token=QUFFLUhqbjdFWE0tYWRiamNld2ZCVm0zajZZc2ctUnVjUXxBQ3Jtc0tsTW9FWHY3Q0VTZTdQTVNkMXlNWTZyQS05OFdxVDgzVG5NNGFSSEVMYzgxa2w1aDV4Wnk5ZkNoN3ppaVo5QjZNRFo3TUU4NTJ0X19sSkt0TWx0VWloYmN6UE5fa2hETEpDd3llYk1mMTVLQ195dVF1Yw%3D%3D&token=vjVQa1PpcFMaMZulhvT3HPF_TNx1Vz8N_KZJB3-DLAQ%3D&keywords=docker%2Cspotify%2Clinux%2Ccontainer%2Copen+source%2Ctwitter%2Ctwitteross%2Cuniversity%2Clearn&allow_ratings=1&subtitles_xlb=http%3A%2F%2Fs.ytimg.com%2Fyts%2Fxlbbin%2Fsubtitles-strings-de_DE-vfl8muEFS.xlb"; };

            var data = new YoutubeMovieData(a, 
                // fixedFile);
                YoutubeMovieData.Download);
                // cachedDownload);

            var video = data.PreferredVideo;

            var path = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                "Downloads", video.Filename);

            var webClient = new WebClient();
            webClient.DownloadFileCompleted += (sender2, asyncCompletedEventArgs) =>
            {
                Console.WriteLine("Video downloaded to {0}", path);
            };

            Console.WriteLine("Download {0}", video.Url.AbsoluteUri);

            webClient.DownloadFile(video.Url, path);
        }
    }
}