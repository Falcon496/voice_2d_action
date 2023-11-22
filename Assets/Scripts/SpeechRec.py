import time
import speech_recognition
import pyaudio

SAMPLERATE = 44100


def callback(in_data, frame_count, time_info, status):
    global sprec 
    try:
        # print("listening")
        audiodata = speech_recognition.AudioData(in_data,SAMPLERATE,2)
        sprec_text = sprec.recognize_google(audiodata, language="ja-jp")
        if sprec_text == "ジャンプ":
            print("jump")
        elif sprec_text == "右":
            print("right")
        elif sprec_text == "左":
            print("left")
        elif sprec_text == "止まれ":
            print("stop")
        else:
            print(sprec_text)
    except speech_recognition.UnknownValueError:
        pass
    except speech_recognition.RequestError as e:
        pass
    finally:
        return (None, pyaudio.paContinue)
    
def main():
    print("start")
    global sprec 
    sprec = speech_recognition.Recognizer()  # インスタンスを生成
    # Audio インスタンス取得
    audio = pyaudio.PyAudio() 
    stream = audio.open( format = pyaudio.paInt16,
                        rate = SAMPLERATE,
                        channels = 1, 
                        input_device_index = 1,
                        input = True, 
                        frames_per_buffer = SAMPLERATE*2, # 秒周期でコールバック
                        stream_callback=callback)
    stream.start_stream()
    while stream.is_active():
        time.sleep(0.1)
    
    stream.stop_stream()
    stream.close()
    audio.terminate()
    

    
if __name__ == '__main__':
    main()