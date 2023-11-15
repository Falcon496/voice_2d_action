#Ctest.py
#1秒ごとに0から9までカウントアップするプログラム。
import time

def main():
    print("start")
    for i in range(10):
        time.sleep(1)
        print("process" ,i*1)

if __name__ == '__main__':
    main()