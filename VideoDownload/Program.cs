namespace Downloader
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
                "https://www.youtube.com/watch?v=SGUpT-a99MA"
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

    public static class MyExtensions
    {
        public static string HtmlDecode(this string str) { return SimpleOwin.Middlewares.Helpers.HttpUtility.HtmlDecode(str); }
        public static Dictionary<string, string> ParseQueryString(this string str) { return SimpleOwin.Middlewares.Helpers.HttpUtility.ParseQueryString(str).AsDictionary(); }

        public static Dictionary<string, string> AsDictionary(this NameValueCollection nvc)
        {
            var result = new Dictionary<string, string>();
            foreach (var key in nvc.Keys)
            {
                result[(string)key] = nvc[(string)key];
            }
            return result;
        }
    }


    public class YoutubeMovieData
    {

        #region properties
        public string account_playback_token { get; set; }
        public NVK adaptive_fmts { get; set; }
        public bool allow_embed { get; set; }
        public bool allow_ratings { get; set; }
        public string author { get; set; }
        public double avg_rating { get; set; }
        public string c { get; set; }
        public NVK caption_audio_tracks { get; set; }
        public NVK caption_tracks { get; set; }
        public NVK caption_translation_languages { get; set; }
        public string cc3_module { get; set; }
        public string cc_asr { get; set; }
        public string cc_font { get; set; }
        public string cc_fonts_url { get; set; }
        public string cc_module { get; set; }
        public string cl { get; set; }
        public string csi_page_type { get; set; }
        public string dashmpd { get; set; }
        public string default_audio_track_index { get; set; }
        public string enablecsi { get; set; }
        public string eventid { get; set; }
        public string fexp { get; set; }
        public string fmt_list { get; set; }
        public string has_cc { get; set; }
        public string hl { get; set; }
        public string idpj { get; set; }
        public string iurl { get; set; }
        public string iurlhq { get; set; }
        public string iurlmaxres { get; set; }
        public string iurlmq { get; set; }
        public string iurlsd { get; set; }
        public string keywords { get; set; }
        public string ldpj { get; set; }
        public string length_seconds { get; set; }
        public string loudness { get; set; }
        public string muted { get; set; }
        public string no_get_video_log { get; set; }
        public string of { get; set; }
        public string plid { get; set; }
        public string pltype { get; set; }
        public string probe_url { get; set; }
        public string ptk { get; set; }
        public string status { get; set; }
        public string storyboard_spec { get; set; }
        public string subtitles_xlb { get; set; }
        public string thumbnail_url { get; set; }
        public string timestamp { get; set; }
        public string title { get; set; }
        public string tmi { get; set; }
        public string token { get; set; }
        public string ttsurl { get; set; }
        public string url_encoded_fmt_stream_map { get; set; }
        public string use_cipher_signature { get; set; }
        public string video_id { get; set; }
        public string video_verticals { get; set; }
        public string view_count { get; set; }
        public string watermark { get; set; }

        #endregion
        public string reason { get; set; }

        public List<Video> Videos { get; set; }

        public Video GetVideoByITag(IEnumerable<int> itags)
        {
            var video = itags
                .Select(itag => this.Videos.FirstOrDefault(_ => _.itag == itag))
                .FirstOrDefault(_ => _ != null);

            return video;
        }

        public Video PreferredVideo
        {
            get
            {
                return this.GetVideoByITag(new[] {
                    22, // hd720 - mp4
                    18, // medium - mp4
                    43 // medium - webm
                });
            }
        }

        public class Video
        {
            public int itag { get; set; }
            public Uri Url { get; set; }
            public string fallback_host { get; set; }
            public string type { get; set; }
            public string quality { get; set; }
            public string Format { get; set; }
            public string Extension { get; set; }
            public string Filename { get; set; }


            public Video(string data, string title)
            {
                var parsed = data.HtmlDecode().ParseQueryString();
                Func<string, string> param = key => parsed[key].HtmlDecode();

                this.itag = int.Parse(param("itag"));
                this.Url = new Uri(param("url"));
                this.type = param("type");
                this.quality = param("quality");
                this.fallback_host = param("fallback_host");

                this.Format = this.quality + " - " + this.type.Split(';')[0].Split('/')[1];
                this.Extension = this.type.Split(';')[0].Split('/')[1];

                title = title.Replace(":", "-");

                this.Filename = string.Format("{0} ({1}).{2}", title, this.Format, this.Extension);
            }
        }

        public YoutubeMovieData(string address, Func<string, string> downloader)
        {
            var id = new Uri(address).Query.ParseQueryString()["v"];
            var infoText = downloader(id);
            var infoParsed = infoText.HtmlDecode().ParseQueryString();

            #region set everything
            this.account_playback_token = infoParsed["account_playback_token"];
            this.adaptive_fmts = infoParsed["adaptive_fmts"].HtmlDecode().ParseQueryString();
            this.allow_embed = 0 != int.Parse(infoParsed["allow_embed"]);
            this.allow_ratings = 0 != int.Parse(infoParsed["allow_ratings"]);
            this.author = infoParsed["author"];
            this.avg_rating = double.Parse(infoParsed["avg_rating"]);
            this.c = infoParsed["c"];
            this.caption_audio_tracks = infoParsed["caption_audio_tracks"].HtmlDecode().ParseQueryString();
            this.caption_tracks = infoParsed["caption_tracks"].HtmlDecode().ParseQueryString();
            this.caption_translation_languages = infoParsed["caption_translation_languages"].HtmlDecode().ParseQueryString();
            this.cc3_module = infoParsed["cc3_module"];
            this.cc_asr = infoParsed["cc_asr"];
            this.cc_font = infoParsed["cc_font"];
            this.cc_fonts_url = infoParsed["cc_fonts_url"];
            this.cc_module = infoParsed["cc_module"];
            this.cl = infoParsed["cl"];
            this.csi_page_type = infoParsed["csi_page_type"];
            this.dashmpd = infoParsed["dashmpd"];
            this.default_audio_track_index = infoParsed["default_audio_track_index"];
            this.enablecsi = infoParsed["enablecsi"];
            this.eventid = infoParsed["eventid"];
            this.fexp = infoParsed["fexp"];
            this.fmt_list = infoParsed["fmt_list"];
            this.has_cc = infoParsed["has_cc"];
            this.hl = infoParsed["hl"];
            this.idpj = infoParsed["idpj"];
            this.iurl = infoParsed["iurl"];
            this.iurlhq = infoParsed["iurlhq"];
            this.iurlmaxres = infoParsed["iurlmaxres"];
            this.iurlmq = infoParsed["iurlmq"];
            this.iurlsd = infoParsed["iurlsd"];
            this.keywords = infoParsed["keywords"];
            this.ldpj = infoParsed["ldpj"];
            this.length_seconds = infoParsed["length_seconds"];
            this.loudness = infoParsed["loudness"];
            this.muted = infoParsed["muted"];
            this.no_get_video_log = infoParsed["no_get_video_log"];
            this.of = infoParsed["of"];
            this.plid = infoParsed["plid"];
            this.pltype = infoParsed["pltype"];
            // this.probe_url = infoParsed["probe_url"];
            this.ptk = infoParsed["ptk"];
            this.status = infoParsed["status"];
            this.storyboard_spec = infoParsed["storyboard_spec"];
            this.subtitles_xlb = infoParsed["subtitles_xlb"];
            this.thumbnail_url = infoParsed["thumbnail_url"];
            this.timestamp = infoParsed["timestamp"];
            this.title = infoParsed["title"];
            this.tmi = infoParsed["tmi"];
            this.token = infoParsed["token"];
            this.ttsurl = infoParsed["ttsurl"];
            this.url_encoded_fmt_stream_map = infoParsed["url_encoded_fmt_stream_map"];
            this.use_cipher_signature = infoParsed["use_cipher_signature"];
            this.video_id = infoParsed["video_id"];
            this.video_verticals = infoParsed["video_verticals"];
            this.view_count = infoParsed["view_count"];
            this.watermark = infoParsed["watermark"];
            #endregion

            this.reason = infoParsed.ContainsKey("reason") ? infoParsed["reason"] : null;
            this.Videos = infoParsed["url_encoded_fmt_stream_map"].Split(',').Select(data => new Video(data, this.title)).ToList();
        }


        public static string Download(string videoId)
        {
            var videoInfoUrl = string.Format("http://www.youtube.com/get_video_info?video_id={0}", videoId);

            var client = new HttpClient();
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, new Uri(videoInfoUrl));
            requestMessage.Headers.Add("X-Forwarded-For", "187.72.88.32");
            var httpResponse = client.SendAsync(requestMessage).Result;
            var x = httpResponse.Content.ReadAsStringAsync().Result;

            var request = (HttpWebRequest)WebRequest.Create(videoInfoUrl);
            var response = (HttpWebResponse)(request.GetResponse());
            var responseStream = response.GetResponseStream();
            var reader = new StreamReader(responseStream, Encoding.UTF8);
            var videoInfoBody = reader.ReadToEnd();
            return videoInfoBody;
        }
    }
}

namespace SimpleOwin.Middlewares.Helpers
{
    // https://raw.githubusercontent.com/prabirshrestha/simple-owin/master/src/SimpleOwin.Middlewares/Helpers/HttpUtility.cs

    // 
    // System.Web.HttpUtility
    //
    // Authors:
    //   Patrik Torstensson (Patrik.Torstensson@labs2.com)
    //   Wictor Wil�n (decode/encode functions) (wictor@ibizkit.se)
    //   Tim Coleman (tim@timcoleman.com)
    //   Gonzalo Paniagua Javier (gonzalo@ximian.com)
    //
    // Copyright (C) 2005-2010 Novell, Inc (http://www.novell.com)
    //
    // Permission is hereby granted, free of charge, to any person obtaining
    // a copy of this software and associated documentation files (the
    // "Software"), to deal in the Software without restriction, including
    // without limitation the rights to use, copy, modify, merge, publish,
    // distribute, sublicense, and/or sell copies of the Software, and to
    // permit persons to whom the Software is furnished to do so, subject to
    // the following conditions:
    // 
    // The above copyright notice and this permission notice shall be
    // included in all copies or substantial portions of the Software.
    // 
    // THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
    // EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
    // MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
    // NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
    // LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
    // OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
    // WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
    //

    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Globalization;
    using System.IO;
    using System.Text;


    public sealed class HttpUtility
    {
        sealed class HttpQSCollection : NameValueCollection
        {
            public override string ToString()
            {
                int count = Count;
                if (count == 0)
                    return "";
                StringBuilder sb = new StringBuilder();
                string[] keys = AllKeys;
                for (int i = 0; i < count; i++)
                {
                    sb.AppendFormat("{0}={1}&", keys[i], this[keys[i]]);
                }
                if (sb.Length > 0)
                    sb.Length--;
                return sb.ToString();
            }
        }

        #region Constructors

        public HttpUtility()
        {
        }

        #endregion // Constructors

        #region Methods

        public static void HtmlAttributeEncode(string s, TextWriter output)
        {
            if (output == null)
            {
#if NET_4_0
				throw new ArgumentNullException ("output");
#else
                throw new NullReferenceException(".NET emulation");
#endif
            }
#if NET_4_0
			HttpEncoder.Current.HtmlAttributeEncode (s, output);
#else
            output.Write(HttpEncoder.HtmlAttributeEncode(s));
#endif
        }

        public static string HtmlAttributeEncode(string s)
        {
#if NET_4_0
			if (s == null)
				return null;
			
			using (var sw = new StringWriter ()) {
				HttpEncoder.Current.HtmlAttributeEncode (s, sw);
				return sw.ToString ();
			}
#else
            return HttpEncoder.HtmlAttributeEncode(s);
#endif
        }

        public static string UrlDecode(string str)
        {
            return UrlDecode(str, Encoding.UTF8);
        }

        static char[] GetChars(MemoryStream b, Encoding e)
        {
            return e.GetChars(b.GetBuffer(), 0, (int)b.Length);
        }

        static void WriteCharBytes(IList buf, char ch, Encoding e)
        {
            if (ch > 255)
            {
                foreach (byte b in e.GetBytes(new char[] { ch }))
                    buf.Add(b);
            }
            else
                buf.Add((byte)ch);
        }

        public static string UrlDecode(string s, Encoding e)
        {
            if (null == s)
                return null;

            if (s.IndexOf('%') == -1 && s.IndexOf('+') == -1)
                return s;

            if (e == null)
                e = Encoding.UTF8;

            long len = s.Length;
            var bytes = new List<byte>();
            int xchar;
            char ch;

            for (int i = 0; i < len; i++)
            {
                ch = s[i];
                if (ch == '%' && i + 2 < len && s[i + 1] != '%')
                {
                    if (s[i + 1] == 'u' && i + 5 < len)
                    {
                        // unicode hex sequence
                        xchar = GetChar(s, i + 2, 4);
                        if (xchar != -1)
                        {
                            WriteCharBytes(bytes, (char)xchar, e);
                            i += 5;
                        }
                        else
                            WriteCharBytes(bytes, '%', e);
                    }
                    else if ((xchar = GetChar(s, i + 1, 2)) != -1)
                    {
                        WriteCharBytes(bytes, (char)xchar, e);
                        i += 2;
                    }
                    else
                    {
                        WriteCharBytes(bytes, '%', e);
                    }
                    continue;
                }

                if (ch == '+')
                    WriteCharBytes(bytes, ' ', e);
                else
                    WriteCharBytes(bytes, ch, e);
            }

            byte[] buf = bytes.ToArray();
            bytes = null;
            return e.GetString(buf);

        }

        public static string UrlDecode(byte[] bytes, Encoding e)
        {
            if (bytes == null)
                return null;

            return UrlDecode(bytes, 0, bytes.Length, e);
        }

        static int GetInt(byte b)
        {
            char c = (char)b;
            if (c >= '0' && c <= '9')
                return c - '0';

            if (c >= 'a' && c <= 'f')
                return c - 'a' + 10;

            if (c >= 'A' && c <= 'F')
                return c - 'A' + 10;

            return -1;
        }

        static int GetChar(byte[] bytes, int offset, int length)
        {
            int value = 0;
            int end = length + offset;
            for (int i = offset; i < end; i++)
            {
                int current = GetInt(bytes[i]);
                if (current == -1)
                    return -1;
                value = (value << 4) + current;
            }

            return value;
        }

        static int GetChar(string str, int offset, int length)
        {
            int val = 0;
            int end = length + offset;
            for (int i = offset; i < end; i++)
            {
                char c = str[i];
                if (c > 127)
                    return -1;

                int current = GetInt((byte)c);
                if (current == -1)
                    return -1;
                val = (val << 4) + current;
            }

            return val;
        }

        public static string UrlDecode(byte[] bytes, int offset, int count, Encoding e)
        {
            if (bytes == null)
                return null;
            if (count == 0)
                return String.Empty;

            if (bytes == null)
                throw new ArgumentNullException("bytes");

            if (offset < 0 || offset > bytes.Length)
                throw new ArgumentOutOfRangeException("offset");

            if (count < 0 || offset + count > bytes.Length)
                throw new ArgumentOutOfRangeException("count");

            StringBuilder output = new StringBuilder();
            MemoryStream acc = new MemoryStream();

            int end = count + offset;
            int xchar;
            for (int i = offset; i < end; i++)
            {
                if (bytes[i] == '%' && i + 2 < count && bytes[i + 1] != '%')
                {
                    if (bytes[i + 1] == (byte)'u' && i + 5 < end)
                    {
                        if (acc.Length > 0)
                        {
                            output.Append(GetChars(acc, e));
                            acc.SetLength(0);
                        }
                        xchar = GetChar(bytes, i + 2, 4);
                        if (xchar != -1)
                        {
                            output.Append((char)xchar);
                            i += 5;
                            continue;
                        }
                    }
                    else if ((xchar = GetChar(bytes, i + 1, 2)) != -1)
                    {
                        acc.WriteByte((byte)xchar);
                        i += 2;
                        continue;
                    }
                }

                if (acc.Length > 0)
                {
                    output.Append(GetChars(acc, e));
                    acc.SetLength(0);
                }

                if (bytes[i] == '+')
                {
                    output.Append(' ');
                }
                else
                {
                    output.Append((char)bytes[i]);
                }
            }

            if (acc.Length > 0)
            {
                output.Append(GetChars(acc, e));
            }

            acc = null;
            return output.ToString();
        }

        public static byte[] UrlDecodeToBytes(byte[] bytes)
        {
            if (bytes == null)
                return null;

            return UrlDecodeToBytes(bytes, 0, bytes.Length);
        }

        public static byte[] UrlDecodeToBytes(string str)
        {
            return UrlDecodeToBytes(str, Encoding.UTF8);
        }

        public static byte[] UrlDecodeToBytes(string str, Encoding e)
        {
            if (str == null)
                return null;

            if (e == null)
                throw new ArgumentNullException("e");

            return UrlDecodeToBytes(e.GetBytes(str));
        }

        public static byte[] UrlDecodeToBytes(byte[] bytes, int offset, int count)
        {
            if (bytes == null)
                return null;
            if (count == 0)
                return new byte[0];

            int len = bytes.Length;
            if (offset < 0 || offset >= len)
                throw new ArgumentOutOfRangeException("offset");

            if (count < 0 || offset > len - count)
                throw new ArgumentOutOfRangeException("count");

            MemoryStream result = new MemoryStream();
            int end = offset + count;
            for (int i = offset; i < end; i++)
            {
                char c = (char)bytes[i];
                if (c == '+')
                {
                    c = ' ';
                }
                else if (c == '%' && i < end - 2)
                {
                    int xchar = GetChar(bytes, i + 1, 2);
                    if (xchar != -1)
                    {
                        c = (char)xchar;
                        i += 2;
                    }
                }
                result.WriteByte((byte)c);
            }

            return result.ToArray();
        }

        public static string UrlEncode(string str)
        {
            return UrlEncode(str, Encoding.UTF8);
        }

        public static string UrlEncode(string s, Encoding Enc)
        {
            if (s == null)
                return null;

            if (s == String.Empty)
                return String.Empty;

            bool needEncode = false;
            int len = s.Length;
            for (int i = 0; i < len; i++)
            {
                char c = s[i];
                if ((c < '0') || (c < 'A' && c > '9') || (c > 'Z' && c < 'a') || (c > 'z'))
                {
                    if (HttpEncoder.NotEncoded(c))
                        continue;

                    needEncode = true;
                    break;
                }
            }

            if (!needEncode)
                return s;

            // avoided GetByteCount call
            byte[] bytes = new byte[Enc.GetMaxByteCount(s.Length)];
            int realLen = Enc.GetBytes(s, 0, s.Length, bytes, 0);
            return Encoding.ASCII.GetString(UrlEncodeToBytes(bytes, 0, realLen));
        }

        public static string UrlEncode(byte[] bytes)
        {
            if (bytes == null)
                return null;

            if (bytes.Length == 0)
                return String.Empty;

            return Encoding.ASCII.GetString(UrlEncodeToBytes(bytes, 0, bytes.Length));
        }

        public static string UrlEncode(byte[] bytes, int offset, int count)
        {
            if (bytes == null)
                return null;

            if (bytes.Length == 0)
                return String.Empty;

            return Encoding.ASCII.GetString(UrlEncodeToBytes(bytes, offset, count));
        }

        public static byte[] UrlEncodeToBytes(string str)
        {
            return UrlEncodeToBytes(str, Encoding.UTF8);
        }

        public static byte[] UrlEncodeToBytes(string str, Encoding e)
        {
            if (str == null)
                return null;

            if (str.Length == 0)
                return new byte[0];

            byte[] bytes = e.GetBytes(str);
            return UrlEncodeToBytes(bytes, 0, bytes.Length);
        }

        public static byte[] UrlEncodeToBytes(byte[] bytes)
        {
            if (bytes == null)
                return null;

            if (bytes.Length == 0)
                return new byte[0];

            return UrlEncodeToBytes(bytes, 0, bytes.Length);
        }

        public static byte[] UrlEncodeToBytes(byte[] bytes, int offset, int count)
        {
            if (bytes == null)
                return null;
#if NET_4_0
			return HttpEncoder.Current.UrlEncode (bytes, offset, count);
#else
            return HttpEncoder.UrlEncodeToBytes(bytes, offset, count);
#endif
        }

        public static string UrlEncodeUnicode(string str)
        {
            if (str == null)
                return null;

            return Encoding.ASCII.GetString(UrlEncodeUnicodeToBytes(str));
        }

        public static byte[] UrlEncodeUnicodeToBytes(string str)
        {
            if (str == null)
                return null;

            if (str.Length == 0)
                return new byte[0];

            MemoryStream result = new MemoryStream(str.Length);
            foreach (char c in str)
            {
                HttpEncoder.UrlEncodeChar(c, result, true);
            }
            return result.ToArray();
        }

        /// <summary>
        /// Decodes an HTML-encoded string and returns the decoded string.
        /// </summary>
        /// <param name="s">The HTML string to decode. </param>
        /// <returns>The decoded text.</returns>
        public static string HtmlDecode(string s)
        {
#if NET_4_0
			if (s == null)
				return null;
			
			using (var sw = new StringWriter ()) {
				HttpEncoder.Current.HtmlDecode (s, sw);
				return sw.ToString ();
			}
#else
            return HttpEncoder.HtmlDecode(s);
#endif
        }

        /// <summary>
        /// Decodes an HTML-encoded string and sends the resulting output to a TextWriter output stream.
        /// </summary>
        /// <param name="s">The HTML string to decode</param>
        /// <param name="output">The TextWriter output stream containing the decoded string. </param>
        public static void HtmlDecode(string s, TextWriter output)
        {
            if (output == null)
            {
#if NET_4_0
				throw new ArgumentNullException ("output");
#else
                throw new NullReferenceException(".NET emulation");
#endif
            }

            if (!String.IsNullOrEmpty(s))
            {
#if NET_4_0
				HttpEncoder.Current.HtmlDecode (s, output);
#else
                output.Write(HttpEncoder.HtmlDecode(s));
#endif
            }
        }

        public static string HtmlEncode(string s)
        {
#if NET_4_0
			if (s == null)
				return null;
			
			using (var sw = new StringWriter ()) {
				HttpEncoder.Current.HtmlEncode (s, sw);
				return sw.ToString ();
			}
#else
            return HttpEncoder.HtmlEncode(s);
#endif
        }

        /// <summary>
        /// HTML-encodes a string and sends the resulting output to a TextWriter output stream.
        /// </summary>
        /// <param name="s">The string to encode. </param>
        /// <param name="output">The TextWriter output stream containing the encoded string. </param>
        public static void HtmlEncode(string s, TextWriter output)
        {
            if (output == null)
            {
#if NET_4_0
				throw new ArgumentNullException ("output");
#else
                throw new NullReferenceException(".NET emulation");
#endif
            }

            if (!String.IsNullOrEmpty(s))
            {
#if NET_4_0
				HttpEncoder.Current.HtmlEncode (s, output);
#else
                output.Write(HttpEncoder.HtmlEncode(s));
#endif
            }
        }
#if NET_4_0
		public static string HtmlEncode (object value)
		{
			if (value == null)
				return null;

			IHtmlString htmlString = value as IHtmlString;
			if (htmlString != null)
				return htmlString.ToHtmlString ();
			
			return HtmlEncode (value.ToString ());
		}

		public static string JavaScriptStringEncode (string value)
		{
			return JavaScriptStringEncode (value, false);
		}

		public static string JavaScriptStringEncode (string value, bool addDoubleQuotes)
		{
			if (String.IsNullOrEmpty (value))
				return addDoubleQuotes ? "\"\"" : String.Empty;

			int len = value.Length;
			bool needEncode = false;
			char c;
			for (int i = 0; i < len; i++) {
				c = value [i];

				if (c >= 0 && c <= 31 || c == 34 || c == 39 || c == 60 || c == 62 || c == 92) {
					needEncode = true;
					break;
				}
			}

			if (!needEncode)
				return addDoubleQuotes ? "\"" + value + "\"" : value;

			var sb = new StringBuilder ();
			if (addDoubleQuotes)
				sb.Append ('"');

			for (int i = 0; i < len; i++) {
				c = value [i];
				if (c >= 0 && c <= 7 || c == 11 || c >= 14 && c <= 31 || c == 39 || c == 60 || c == 62)
					sb.AppendFormat ("\\u{0:x4}", (int)c);
				else switch ((int)c) {
						case 8:
							sb.Append ("\\b");
							break;

						case 9:
							sb.Append ("\\t");
							break;

						case 10:
							sb.Append ("\\n");
							break;

						case 12:
							sb.Append ("\\f");
							break;

						case 13:
							sb.Append ("\\r");
							break;

						case 34:
							sb.Append ("\\\"");
							break;

						case 92:
							sb.Append ("\\\\");
							break;

						default:
							sb.Append (c);
							break;
					}
			}

			if (addDoubleQuotes)
				sb.Append ('"');

			return sb.ToString ();
		}
#endif
        public static string UrlPathEncode(string s)
        {
#if NET_4_0
			return HttpEncoder.Current.UrlPathEncode (s);
#else
            return HttpEncoder.UrlPathEncode(s);
#endif
        }

        public static NameValueCollection ParseQueryString(string query)
        {
            return ParseQueryString(query, Encoding.UTF8);
        }

        public static NameValueCollection ParseQueryString(string query, Encoding encoding)
        {
            if (query == null)
                throw new ArgumentNullException("query");
            if (encoding == null)
                throw new ArgumentNullException("encoding");
            if (query.Length == 0 || (query.Length == 1 && query[0] == '?'))
                return new NameValueCollection();
            if (query[0] == '?')
                query = query.Substring(1);

            NameValueCollection result = new HttpQSCollection();
            ParseQueryString(query, encoding, result);
            return result;
        }

        internal static void ParseQueryString(string query, Encoding encoding, NameValueCollection result)
        {
            if (query.Length == 0)
                return;

            string decoded = HtmlDecode(query);
            int decodedLength = decoded.Length;
            int namePos = 0;
            bool first = true;
            while (namePos <= decodedLength)
            {
                int valuePos = -1, valueEnd = -1;
                for (int q = namePos; q < decodedLength; q++)
                {
                    if (valuePos == -1 && decoded[q] == '=')
                    {
                        valuePos = q + 1;
                    }
                    else if (decoded[q] == '&')
                    {
                        valueEnd = q;
                        break;
                    }
                }

                if (first)
                {
                    first = false;
                    if (decoded[namePos] == '?')
                        namePos++;
                }

                string name, value;
                if (valuePos == -1)
                {
                    name = null;
                    valuePos = namePos;
                }
                else
                {
                    name = UrlDecode(decoded.Substring(namePos, valuePos - namePos - 1), encoding);
                }
                if (valueEnd < 0)
                {
                    namePos = -1;
                    valueEnd = decoded.Length;
                }
                else
                {
                    namePos = valueEnd + 1;
                }
                value = UrlDecode(decoded.Substring(valuePos, valueEnd - valuePos), encoding);

                result.Add(name, value);
                if (namePos == -1)
                    break;
            }
        }
        #endregion // Methods
    }

    //
    // Authors:
    //   Patrik Torstensson (Patrik.Torstensson@labs2.com)
    //   Wictor Wil�n (decode/encode functions) (wictor@ibizkit.se)
    //   Tim Coleman (tim@timcoleman.com)
    //   Gonzalo Paniagua Javier (gonzalo@ximian.com)

    //   Marek Habersack <mhabersack@novell.com>
    //
    // (C) 2005-2010 Novell, Inc (http://novell.com/)
    //

    //
    // Permission is hereby granted, free of charge, to any person obtaining
    // a copy of this software and associated documentation files (the
    // "Software"), to deal in the Software without restriction, including
    // without limitation the rights to use, copy, modify, merge, publish,
    // distribute, sublicense, and/or sell copies of the Software, and to
    // permit persons to whom the Software is furnished to do so, subject to
    // the following conditions:
    // 
    // The above copyright notice and this permission notice shall be
    // included in all copies or substantial portions of the Software.
    // 
    // THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND,
    // EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF
    // MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND
    // NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE
    // LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION
    // OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION
    // WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
    //
#if NET_4_0
	public
#endif
    class HttpEncoder
    {
        static char[] hexChars = "0123456789abcdef".ToCharArray();
        static object entitiesLock = new object();
        static SortedDictionary<string, char> entities;
#if NET_4_0
		static Lazy <HttpEncoder> defaultEncoder;
		static Lazy <HttpEncoder> currentEncoderLazy;
#else
        static HttpEncoder defaultEncoder;
#endif
        static HttpEncoder currentEncoder;

        static IDictionary<string, char> Entities
        {
            get
            {
                lock (entitiesLock)
                {
                    if (entities == null)
                        InitEntities();

                    return entities;
                }
            }
        }

        public static HttpEncoder Current
        {
            get
            {
#if NET_4_0
				if (currentEncoder == null)
					currentEncoder = currentEncoderLazy.Value;
#endif
                return currentEncoder;
            }
#if NET_4_0
			set {
				if (value == null)
					throw new ArgumentNullException ("value");
				currentEncoder = value;
			}
#endif
        }

        public static HttpEncoder Default
        {
            get
            {
#if NET_4_0
				return defaultEncoder.Value;
#else
                return defaultEncoder;
#endif
            }
        }

        static HttpEncoder()
        {
#if NET_4_0
			defaultEncoder = new Lazy <HttpEncoder> (() => new HttpEncoder ());
			currentEncoderLazy = new Lazy <HttpEncoder> (new Func <HttpEncoder> (GetCustomEncoderFromConfig));
#else
            defaultEncoder = new HttpEncoder();
            currentEncoder = defaultEncoder;
#endif
        }

        public HttpEncoder()
        {
        }
#if NET_4_0	
		protected internal virtual
#else
        internal static
#endif
 void HeaderNameValueEncode(string headerName, string headerValue, out string encodedHeaderName, out string encodedHeaderValue)
        {
            if (String.IsNullOrEmpty(headerName))
                encodedHeaderName = headerName;
            else
                encodedHeaderName = EncodeHeaderString(headerName);

            if (String.IsNullOrEmpty(headerValue))
                encodedHeaderValue = headerValue;
            else
                encodedHeaderValue = EncodeHeaderString(headerValue);
        }

        static void StringBuilderAppend(string s, ref StringBuilder sb)
        {
            if (sb == null)
                sb = new StringBuilder(s);
            else
                sb.Append(s);
        }

        static string EncodeHeaderString(string input)
        {
            StringBuilder sb = null;
            char ch;

            for (int i = 0; i < input.Length; i++)
            {
                ch = input[i];

                if ((ch < 32 && ch != 9) || ch == 127)
                    StringBuilderAppend(String.Format("%{0:x2}", (int)ch), ref sb);
            }

            if (sb != null)
                return sb.ToString();

            return input;
        }
#if NET_4_0		
		protected internal virtual void HtmlAttributeEncode (string value, TextWriter output)
		{

			if (output == null)
				throw new ArgumentNullException ("output");

			if (String.IsNullOrEmpty (value))
				return;

			output.Write (HtmlAttributeEncode (value));
		}

		protected internal virtual void HtmlDecode (string value, TextWriter output)
		{
			if (output == null)
				throw new ArgumentNullException ("output");

			output.Write (HtmlDecode (value));
		}

		protected internal virtual void HtmlEncode (string value, TextWriter output)
		{
			if (output == null)
				throw new ArgumentNullException ("output");

			output.Write (HtmlEncode (value));
		}

		protected internal virtual byte[] UrlEncode (byte[] bytes, int offset, int count)
		{
			return UrlEncodeToBytes (bytes, offset, count);
		}

		static HttpEncoder GetCustomEncoderFromConfig ()
		{
			var cfg = HttpRuntime.Section;
			string typeName = cfg.EncoderType;

			if (String.Compare (typeName, "System.Web.Util.HttpEncoder", StringComparison.OrdinalIgnoreCase) == 0)
				return Default;
			
			Type t = Type.GetType (typeName, false);
			if (t == null)
				throw new ConfigurationErrorsException (String.Format ("Could not load type '{0}'.", typeName));
			
			if (!typeof (HttpEncoder).IsAssignableFrom (t))
				throw new ConfigurationErrorsException (
					String.Format ("'{0}' is not allowed here because it does not extend class 'System.Web.Util.HttpEncoder'.", typeName)
				);

			return Activator.CreateInstance (t, false) as HttpEncoder;
		}
#endif
#if NET_4_0
		protected internal virtual
#else
        internal static
#endif
 string UrlPathEncode(string value)
        {
            if (String.IsNullOrEmpty(value))
                return value;

            MemoryStream result = new MemoryStream();
            int length = value.Length;
            for (int i = 0; i < length; i++)
                UrlPathEncodeChar(value[i], result);

            return Encoding.ASCII.GetString(result.ToArray());
        }

        internal static byte[] UrlEncodeToBytes(byte[] bytes, int offset, int count)
        {
            if (bytes == null)
                throw new ArgumentNullException("bytes");

            int blen = bytes.Length;
            if (blen == 0)
                return new byte[0];

            if (offset < 0 || offset >= blen)
                throw new ArgumentOutOfRangeException("offset");

            if (count < 0 || count > blen - offset)
                throw new ArgumentOutOfRangeException("count");

            MemoryStream result = new MemoryStream(count);
            int end = offset + count;
            for (int i = offset; i < end; i++)
                UrlEncodeChar((char)bytes[i], result, false);

            return result.ToArray();
        }

        internal static string HtmlEncode(string s)
        {
            if (s == null)
                return null;

            if (s.Length == 0)
                return String.Empty;

            bool needEncode = false;
            for (int i = 0; i < s.Length; i++)
            {
                char c = s[i];
                if (c == '&' || c == '"' || c == '<' || c == '>' || c > 159
#if NET_4_0
					|| c == '\''
#endif
)
                {
                    needEncode = true;
                    break;
                }
            }

            if (!needEncode)
                return s;

            StringBuilder output = new StringBuilder();
            char ch;
            int len = s.Length;

            for (int i = 0; i < len; i++)
            {
                switch (s[i])
                {
                    case '&':
                        output.Append("&amp;");
                        break;
                    case '>':
                        output.Append("&gt;");
                        break;
                    case '<':
                        output.Append("&lt;");
                        break;
                    case '"':
                        output.Append("&quot;");
                        break;
#if NET_4_0
					case '\'':
						output.Append ("&#39;");
						break;
#endif
                    case '\uff1c':
                        output.Append("&#65308;");
                        break;

                    case '\uff1e':
                        output.Append("&#65310;");
                        break;

                    default:
                        ch = s[i];
                        if (ch > 159 && ch < 256)
                        {
                            output.Append("&#");
                            output.Append(((int)ch).ToString(CultureInfo.InvariantCulture));
                            output.Append(";");
                        }
                        else
                            output.Append(ch);
                        break;
                }
            }

            return output.ToString();
        }

        internal static string HtmlAttributeEncode(string s)
        {
#if NET_4_0
			if (String.IsNullOrEmpty (s))
				return String.Empty;
#else
            if (s == null)
                return null;

            if (s.Length == 0)
                return String.Empty;
#endif
            bool needEncode = false;
            for (int i = 0; i < s.Length; i++)
            {
                char c = s[i];
                if (c == '&' || c == '"' || c == '<'
#if NET_4_0
					|| c == '\''
#endif
)
                {
                    needEncode = true;
                    break;
                }
            }

            if (!needEncode)
                return s;

            StringBuilder output = new StringBuilder();
            int len = s.Length;
            for (int i = 0; i < len; i++)
                switch (s[i])
                {
                    case '&':
                        output.Append("&amp;");
                        break;
                    case '"':
                        output.Append("&quot;");
                        break;
                    case '<':
                        output.Append("&lt;");
                        break;
#if NET_4_0
				case '\'':
					output.Append ("&#39;");
					break;
#endif
                    default:
                        output.Append(s[i]);
                        break;
                }

            return output.ToString();
        }

        internal static string HtmlDecode(string s)
        {
            if (s == null)
                return null;

            if (s.Length == 0)
                return String.Empty;

            if (s.IndexOf('&') == -1)
                return s;
#if NET_4_0
			StringBuilder rawEntity = new StringBuilder ();
#endif
            StringBuilder entity = new StringBuilder();
            StringBuilder output = new StringBuilder();
            int len = s.Length;
            // 0 -> nothing,
            // 1 -> right after '&'
            // 2 -> between '&' and ';' but no '#'
            // 3 -> '#' found after '&' and getting numbers
            int state = 0;
            int number = 0;
            bool is_hex_value = false;
            bool have_trailing_digits = false;

            for (int i = 0; i < len; i++)
            {
                char c = s[i];
                if (state == 0)
                {
                    if (c == '&')
                    {
                        entity.Append(c);
#if NET_4_0
						rawEntity.Append (c);
#endif
                        state = 1;
                    }
                    else
                    {
                        output.Append(c);
                    }
                    continue;
                }

                if (c == '&')
                {
                    state = 1;
                    if (have_trailing_digits)
                    {
                        entity.Append(number.ToString(CultureInfo.InvariantCulture));
                        have_trailing_digits = false;
                    }

                    output.Append(entity.ToString());
                    entity.Length = 0;
                    entity.Append('&');
                    continue;
                }

                if (state == 1)
                {
                    if (c == ';')
                    {
                        state = 0;
                        output.Append(entity.ToString());
                        output.Append(c);
                        entity.Length = 0;
                    }
                    else
                    {
                        number = 0;
                        is_hex_value = false;
                        if (c != '#')
                        {
                            state = 2;
                        }
                        else
                        {
                            state = 3;
                        }
                        entity.Append(c);
#if NET_4_0
						rawEntity.Append (c);
#endif
                    }
                }
                else if (state == 2)
                {
                    entity.Append(c);
                    if (c == ';')
                    {
                        string key = entity.ToString();
                        if (key.Length > 1 && Entities.ContainsKey(key.Substring(1, key.Length - 2)))
                            key = Entities[key.Substring(1, key.Length - 2)].ToString();

                        output.Append(key);
                        state = 0;
                        entity.Length = 0;
#if NET_4_0
						rawEntity.Length = 0;
#endif
                    }
                }
                else if (state == 3)
                {
                    if (c == ';')
                    {
#if NET_4_0
						if (number == 0)
							output.Append (rawEntity.ToString () + ";");
						else
#endif
                        if (number > 65535)
                        {
                            output.Append("&#");
                            output.Append(number.ToString(CultureInfo.InvariantCulture));
                            output.Append(";");
                        }
                        else
                        {
                            output.Append((char)number);
                        }
                        state = 0;
                        entity.Length = 0;
#if NET_4_0
						rawEntity.Length = 0;
#endif
                        have_trailing_digits = false;
                    }
                    else if (is_hex_value && Uri.IsHexDigit(c))
                    {
                        number = number * 16 + Uri.FromHex(c);
                        have_trailing_digits = true;
#if NET_4_0
						rawEntity.Append (c);
#endif
                    }
                    else if (Char.IsDigit(c))
                    {
                        number = number * 10 + ((int)c - '0');
                        have_trailing_digits = true;
#if NET_4_0
						rawEntity.Append (c);
#endif
                    }
                    else if (number == 0 && (c == 'x' || c == 'X'))
                    {
                        is_hex_value = true;
#if NET_4_0
						rawEntity.Append (c);
#endif
                    }
                    else
                    {
                        state = 2;
                        if (have_trailing_digits)
                        {
                            entity.Append(number.ToString(CultureInfo.InvariantCulture));
                            have_trailing_digits = false;
                        }
                        entity.Append(c);
                    }
                }
            }

            if (entity.Length > 0)
            {
                output.Append(entity.ToString());
            }
            else if (have_trailing_digits)
            {
                output.Append(number.ToString(CultureInfo.InvariantCulture));
            }
            return output.ToString();
        }

        internal static bool NotEncoded(char c)
        {
            return (c == '!' || c == '(' || c == ')' || c == '*' || c == '-' || c == '.' || c == '_'
#if !NET_4_0
 || c == '\''
#endif
);
        }

        internal static void UrlEncodeChar(char c, Stream result, bool isUnicode)
        {
            if (c > 255)
            {
                //FIXME: what happens when there is an internal error?
                //if (!isUnicode)
                //	throw new ArgumentOutOfRangeException ("c", c, "c must be less than 256");
                int idx;
                int i = (int)c;

                result.WriteByte((byte)'%');
                result.WriteByte((byte)'u');
                idx = i >> 12;
                result.WriteByte((byte)hexChars[idx]);
                idx = (i >> 8) & 0x0F;
                result.WriteByte((byte)hexChars[idx]);
                idx = (i >> 4) & 0x0F;
                result.WriteByte((byte)hexChars[idx]);
                idx = i & 0x0F;
                result.WriteByte((byte)hexChars[idx]);
                return;
            }

            if (c > ' ' && NotEncoded(c))
            {
                result.WriteByte((byte)c);
                return;
            }
            if (c == ' ')
            {
                result.WriteByte((byte)'+');
                return;
            }
            if ((c < '0') ||
                (c < 'A' && c > '9') ||
                (c > 'Z' && c < 'a') ||
                (c > 'z'))
            {
                if (isUnicode && c > 127)
                {
                    result.WriteByte((byte)'%');
                    result.WriteByte((byte)'u');
                    result.WriteByte((byte)'0');
                    result.WriteByte((byte)'0');
                }
                else
                    result.WriteByte((byte)'%');

                int idx = ((int)c) >> 4;
                result.WriteByte((byte)hexChars[idx]);
                idx = ((int)c) & 0x0F;
                result.WriteByte((byte)hexChars[idx]);
            }
            else
                result.WriteByte((byte)c);
        }

        internal static void UrlPathEncodeChar(char c, Stream result)
        {
            if (c < 33 || c > 126)
            {
                byte[] bIn = Encoding.UTF8.GetBytes(c.ToString());
                for (int i = 0; i < bIn.Length; i++)
                {
                    result.WriteByte((byte)'%');
                    int idx = ((int)bIn[i]) >> 4;
                    result.WriteByte((byte)hexChars[idx]);
                    idx = ((int)bIn[i]) & 0x0F;
                    result.WriteByte((byte)hexChars[idx]);
                }
            }
            else if (c == ' ')
            {
                result.WriteByte((byte)'%');
                result.WriteByte((byte)'2');
                result.WriteByte((byte)'0');
            }
            else
                result.WriteByte((byte)c);
        }

        static void InitEntities()
        {
            // Build the hash table of HTML entity references.  This list comes
            // from the HTML 4.01 W3C recommendation.
            entities = new SortedDictionary<string, char>(StringComparer.Ordinal);

            entities.Add("nbsp", '\u00A0');
            entities.Add("iexcl", '\u00A1');
            entities.Add("cent", '\u00A2');
            entities.Add("pound", '\u00A3');
            entities.Add("curren", '\u00A4');
            entities.Add("yen", '\u00A5');
            entities.Add("brvbar", '\u00A6');
            entities.Add("sect", '\u00A7');
            entities.Add("uml", '\u00A8');
            entities.Add("copy", '\u00A9');
            entities.Add("ordf", '\u00AA');
            entities.Add("laquo", '\u00AB');
            entities.Add("not", '\u00AC');
            entities.Add("shy", '\u00AD');
            entities.Add("reg", '\u00AE');
            entities.Add("macr", '\u00AF');
            entities.Add("deg", '\u00B0');
            entities.Add("plusmn", '\u00B1');
            entities.Add("sup2", '\u00B2');
            entities.Add("sup3", '\u00B3');
            entities.Add("acute", '\u00B4');
            entities.Add("micro", '\u00B5');
            entities.Add("para", '\u00B6');
            entities.Add("middot", '\u00B7');
            entities.Add("cedil", '\u00B8');
            entities.Add("sup1", '\u00B9');
            entities.Add("ordm", '\u00BA');
            entities.Add("raquo", '\u00BB');
            entities.Add("frac14", '\u00BC');
            entities.Add("frac12", '\u00BD');
            entities.Add("frac34", '\u00BE');
            entities.Add("iquest", '\u00BF');
            entities.Add("Agrave", '\u00C0');
            entities.Add("Aacute", '\u00C1');
            entities.Add("Acirc", '\u00C2');
            entities.Add("Atilde", '\u00C3');
            entities.Add("Auml", '\u00C4');
            entities.Add("Aring", '\u00C5');
            entities.Add("AElig", '\u00C6');
            entities.Add("Ccedil", '\u00C7');
            entities.Add("Egrave", '\u00C8');
            entities.Add("Eacute", '\u00C9');
            entities.Add("Ecirc", '\u00CA');
            entities.Add("Euml", '\u00CB');
            entities.Add("Igrave", '\u00CC');
            entities.Add("Iacute", '\u00CD');
            entities.Add("Icirc", '\u00CE');
            entities.Add("Iuml", '\u00CF');
            entities.Add("ETH", '\u00D0');
            entities.Add("Ntilde", '\u00D1');
            entities.Add("Ograve", '\u00D2');
            entities.Add("Oacute", '\u00D3');
            entities.Add("Ocirc", '\u00D4');
            entities.Add("Otilde", '\u00D5');
            entities.Add("Ouml", '\u00D6');
            entities.Add("times", '\u00D7');
            entities.Add("Oslash", '\u00D8');
            entities.Add("Ugrave", '\u00D9');
            entities.Add("Uacute", '\u00DA');
            entities.Add("Ucirc", '\u00DB');
            entities.Add("Uuml", '\u00DC');
            entities.Add("Yacute", '\u00DD');
            entities.Add("THORN", '\u00DE');
            entities.Add("szlig", '\u00DF');
            entities.Add("agrave", '\u00E0');
            entities.Add("aacute", '\u00E1');
            entities.Add("acirc", '\u00E2');
            entities.Add("atilde", '\u00E3');
            entities.Add("auml", '\u00E4');
            entities.Add("aring", '\u00E5');
            entities.Add("aelig", '\u00E6');
            entities.Add("ccedil", '\u00E7');
            entities.Add("egrave", '\u00E8');
            entities.Add("eacute", '\u00E9');
            entities.Add("ecirc", '\u00EA');
            entities.Add("euml", '\u00EB');
            entities.Add("igrave", '\u00EC');
            entities.Add("iacute", '\u00ED');
            entities.Add("icirc", '\u00EE');
            entities.Add("iuml", '\u00EF');
            entities.Add("eth", '\u00F0');
            entities.Add("ntilde", '\u00F1');
            entities.Add("ograve", '\u00F2');
            entities.Add("oacute", '\u00F3');
            entities.Add("ocirc", '\u00F4');
            entities.Add("otilde", '\u00F5');
            entities.Add("ouml", '\u00F6');
            entities.Add("divide", '\u00F7');
            entities.Add("oslash", '\u00F8');
            entities.Add("ugrave", '\u00F9');
            entities.Add("uacute", '\u00FA');
            entities.Add("ucirc", '\u00FB');
            entities.Add("uuml", '\u00FC');
            entities.Add("yacute", '\u00FD');
            entities.Add("thorn", '\u00FE');
            entities.Add("yuml", '\u00FF');
            entities.Add("fnof", '\u0192');
            entities.Add("Alpha", '\u0391');
            entities.Add("Beta", '\u0392');
            entities.Add("Gamma", '\u0393');
            entities.Add("Delta", '\u0394');
            entities.Add("Epsilon", '\u0395');
            entities.Add("Zeta", '\u0396');
            entities.Add("Eta", '\u0397');
            entities.Add("Theta", '\u0398');
            entities.Add("Iota", '\u0399');
            entities.Add("Kappa", '\u039A');
            entities.Add("Lambda", '\u039B');
            entities.Add("Mu", '\u039C');
            entities.Add("Nu", '\u039D');
            entities.Add("Xi", '\u039E');
            entities.Add("Omicron", '\u039F');
            entities.Add("Pi", '\u03A0');
            entities.Add("Rho", '\u03A1');
            entities.Add("Sigma", '\u03A3');
            entities.Add("Tau", '\u03A4');
            entities.Add("Upsilon", '\u03A5');
            entities.Add("Phi", '\u03A6');
            entities.Add("Chi", '\u03A7');
            entities.Add("Psi", '\u03A8');
            entities.Add("Omega", '\u03A9');
            entities.Add("alpha", '\u03B1');
            entities.Add("beta", '\u03B2');
            entities.Add("gamma", '\u03B3');
            entities.Add("delta", '\u03B4');
            entities.Add("epsilon", '\u03B5');
            entities.Add("zeta", '\u03B6');
            entities.Add("eta", '\u03B7');
            entities.Add("theta", '\u03B8');
            entities.Add("iota", '\u03B9');
            entities.Add("kappa", '\u03BA');
            entities.Add("lambda", '\u03BB');
            entities.Add("mu", '\u03BC');
            entities.Add("nu", '\u03BD');
            entities.Add("xi", '\u03BE');
            entities.Add("omicron", '\u03BF');
            entities.Add("pi", '\u03C0');
            entities.Add("rho", '\u03C1');
            entities.Add("sigmaf", '\u03C2');
            entities.Add("sigma", '\u03C3');
            entities.Add("tau", '\u03C4');
            entities.Add("upsilon", '\u03C5');
            entities.Add("phi", '\u03C6');
            entities.Add("chi", '\u03C7');
            entities.Add("psi", '\u03C8');
            entities.Add("omega", '\u03C9');
            entities.Add("thetasym", '\u03D1');
            entities.Add("upsih", '\u03D2');
            entities.Add("piv", '\u03D6');
            entities.Add("bull", '\u2022');
            entities.Add("hellip", '\u2026');
            entities.Add("prime", '\u2032');
            entities.Add("Prime", '\u2033');
            entities.Add("oline", '\u203E');
            entities.Add("frasl", '\u2044');
            entities.Add("weierp", '\u2118');
            entities.Add("image", '\u2111');
            entities.Add("real", '\u211C');
            entities.Add("trade", '\u2122');
            entities.Add("alefsym", '\u2135');
            entities.Add("larr", '\u2190');
            entities.Add("uarr", '\u2191');
            entities.Add("rarr", '\u2192');
            entities.Add("darr", '\u2193');
            entities.Add("harr", '\u2194');
            entities.Add("crarr", '\u21B5');
            entities.Add("lArr", '\u21D0');
            entities.Add("uArr", '\u21D1');
            entities.Add("rArr", '\u21D2');
            entities.Add("dArr", '\u21D3');
            entities.Add("hArr", '\u21D4');
            entities.Add("forall", '\u2200');
            entities.Add("part", '\u2202');
            entities.Add("exist", '\u2203');
            entities.Add("empty", '\u2205');
            entities.Add("nabla", '\u2207');
            entities.Add("isin", '\u2208');
            entities.Add("notin", '\u2209');
            entities.Add("ni", '\u220B');
            entities.Add("prod", '\u220F');
            entities.Add("sum", '\u2211');
            entities.Add("minus", '\u2212');
            entities.Add("lowast", '\u2217');
            entities.Add("radic", '\u221A');
            entities.Add("prop", '\u221D');
            entities.Add("infin", '\u221E');
            entities.Add("ang", '\u2220');
            entities.Add("and", '\u2227');
            entities.Add("or", '\u2228');
            entities.Add("cap", '\u2229');
            entities.Add("cup", '\u222A');
            entities.Add("int", '\u222B');
            entities.Add("there4", '\u2234');
            entities.Add("sim", '\u223C');
            entities.Add("cong", '\u2245');
            entities.Add("asymp", '\u2248');
            entities.Add("ne", '\u2260');
            entities.Add("equiv", '\u2261');
            entities.Add("le", '\u2264');
            entities.Add("ge", '\u2265');
            entities.Add("sub", '\u2282');
            entities.Add("sup", '\u2283');
            entities.Add("nsub", '\u2284');
            entities.Add("sube", '\u2286');
            entities.Add("supe", '\u2287');
            entities.Add("oplus", '\u2295');
            entities.Add("otimes", '\u2297');
            entities.Add("perp", '\u22A5');
            entities.Add("sdot", '\u22C5');
            entities.Add("lceil", '\u2308');
            entities.Add("rceil", '\u2309');
            entities.Add("lfloor", '\u230A');
            entities.Add("rfloor", '\u230B');
            entities.Add("lang", '\u2329');
            entities.Add("rang", '\u232A');
            entities.Add("loz", '\u25CA');
            entities.Add("spades", '\u2660');
            entities.Add("clubs", '\u2663');
            entities.Add("hearts", '\u2665');
            entities.Add("diams", '\u2666');
            entities.Add("quot", '\u0022');
            entities.Add("amp", '\u0026');
            entities.Add("lt", '\u003C');
            entities.Add("gt", '\u003E');
            entities.Add("OElig", '\u0152');
            entities.Add("oelig", '\u0153');
            entities.Add("Scaron", '\u0160');
            entities.Add("scaron", '\u0161');
            entities.Add("Yuml", '\u0178');
            entities.Add("circ", '\u02C6');
            entities.Add("tilde", '\u02DC');
            entities.Add("ensp", '\u2002');
            entities.Add("emsp", '\u2003');
            entities.Add("thinsp", '\u2009');
            entities.Add("zwnj", '\u200C');
            entities.Add("zwj", '\u200D');
            entities.Add("lrm", '\u200E');
            entities.Add("rlm", '\u200F');
            entities.Add("ndash", '\u2013');
            entities.Add("mdash", '\u2014');
            entities.Add("lsquo", '\u2018');
            entities.Add("rsquo", '\u2019');
            entities.Add("sbquo", '\u201A');
            entities.Add("ldquo", '\u201C');
            entities.Add("rdquo", '\u201D');
            entities.Add("bdquo", '\u201E');
            entities.Add("dagger", '\u2020');
            entities.Add("Dagger", '\u2021');
            entities.Add("permil", '\u2030');
            entities.Add("lsaquo", '\u2039');
            entities.Add("rsaquo", '\u203A');
            entities.Add("euro", '\u20AC');
        }
    }
}