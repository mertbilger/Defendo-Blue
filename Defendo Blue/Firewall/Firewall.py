import os
import sys
import time
from collections import defaultdict
from scapy.all import sniff, IP, TCP
import ctypes

THRESHOLD = 100 
BLOCK_DURATION = 90
print(f"THRESHOLD: {THRESHOLD}, BLOCK_DURATION: {BLOCK_DURATION}")

def ensure_file_exists(filename):
    if not os.path.isfile(filename):
        print(f"{filename} bulunamadi. Bos bir dosya olusturuluyor.")
        with open(filename, "w") as file:
            pass
    return os.path.abspath(filename)

def read_ip_file(filename):
    filename = ensure_file_exists(filename)
    with open(filename, "r") as file:
        ips = [line.strip() for line in file]
    return set(ips), filename

def is_nimda_worm(packet):
    if packet.haslayer(TCP) and packet[TCP].dport == 80:
        payload = packet[TCP].payload
        return "GET /scripts/root.exe" in str(payload)
    return False

def log_event(message):
    log_folder = "logs"
    os.makedirs(log_folder, exist_ok=True)
    timestamp = time.strftime("%Y-%m-%d_%H-%M-%S", time.localtime())
    log_file = os.path.join(log_folder, f"log_{timestamp}.txt")
    
    with open(log_file, "a") as file:
        file.write(f"{message}\n")
    print(f"Log Kaydedildi: {os.path.abspath(log_file)}")

def block_ip_windows(ip):
    command = f"netsh advfirewall firewall add rule name=\"Block IP {ip}\" dir=in action=block remoteip={ip}"
    os.system(command)
    log_event(f"Blocking IP {ip} with Windows Firewall")

def unblock_ip_windows(ip):
    command = f"netsh advfirewall firewall delete rule name=\"Block IP {ip}\""
    os.system(command)
    log_event(f"Unblocking IP {ip} from Windows Firewall")

def packet_callback(packet):
    src_ip = packet[IP].src

    if src_ip in whitelist_ips:
        return

    packet_count[src_ip] += 1

    current_time = time.time()
    time_interval = current_time - start_time[0]

    if time_interval >= 1:
        for ip, count in packet_count.items():
            packet_rate = count / time_interval

            if packet_rate > THRESHOLD:
                if ip not in blocked_ips:
                    print(f"Blocking IP: {ip}, Paket Hizi: {packet_rate}")
                    block_ip_windows(ip)
                    blocked_ips[ip] = time.time()

        packet_count.clear()
        start_time[0] = current_time

    for ip in list(blocked_ips.keys()):
        if current_time - blocked_ips[ip] >= BLOCK_DURATION:
            print(f"Unblocking IP: {ip}")
            unblock_ip_windows(ip)
            del blocked_ips[ip]

def is_admin():
    try:
        return ctypes.windll.shell32.IsUserAnAdmin()
    except:
        return False

if __name__ == "__main__":
    if not is_admin():
        print("Bu kod yönetici ayricaliklari gerektirir.")
        sys.exit(1)

    blacklist_file = ensure_file_exists("blacklist.txt")
    whitelist_file = ensure_file_exists("whitelist.txt")

    whitelist_ips, _ = read_ip_file(whitelist_file)

    packet_count = defaultdict(int)
    start_time = [time.time()]
    blocked_ips = {}

    print("Ag Trafigi Izleniyor... ")
    print(f"Whitelist dosya yolu: {whitelist_file}")
    sniff(filter="ip", prn=packet_callback)
