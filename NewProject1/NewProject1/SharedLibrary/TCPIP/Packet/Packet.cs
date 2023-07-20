using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SharedLibrary.TCPIP.Packet
{
    public class Packet
    {
        public PacketHeadItem Head { get; set; }
        public PacketTail Tail { get; set; }

        public Packet() // 디폴트 생성자 추가
        {
        }

        //[JsonConstructor] // 역직렬화 시 생성자 매개변수 무시
        public Packet(PacketHeadItem packetHead, PacketTail packetTail)
        {
            //Head = new PacketHead { PacketType = packetType };
            //Tail = new PacketTail { TailData = tailData };
            Head = packetHead;
            Tail = packetTail;
        }

        public byte[] ToByteArray()
        {
            var jsonSerializerOptions = new JsonSerializerOptions
            {
                WriteIndented = true // 이 옵션은 JSON 출력을 읽기 쉬운 형식으로 설정 (선택 사항)
            };

            var jsonBytes = JsonSerializer.SerializeToUtf8Bytes(this, jsonSerializerOptions);
            return jsonBytes;
        }

        public static Packet FromByteArray(byte[] bytes, int index, int count)
        {
            var jsonSerializerOptions = new JsonSerializerOptions
            {
                WriteIndented = true // 이 옵션은 JSON 출력을 읽기 쉬운 형식으로 설정 (선택 사항)
            };

            string jsonString = Encoding.UTF8.GetString(bytes, index, count);
            return JsonSerializer.Deserialize<Packet>(jsonString, jsonSerializerOptions) ?? new Packet();
        }
    }
}
