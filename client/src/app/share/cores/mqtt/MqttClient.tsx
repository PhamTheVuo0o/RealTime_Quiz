import { useEffect, useState } from 'react';
import mqtt, { MqttClient } from 'mqtt';
import { API_URL } from '@ShareConstants';

export const useMqtt = (topic:string) => {
    const [client, setClient] = useState<MqttClient | null>(null);
    const [isConnected, setIsConnected] = useState(false);
    const [messages, setMessages] = useState<string[]>([]);

    useEffect(() => {
        // Tạo kết nối tới MQTT broker
        const mqttClient = mqtt.connect(API_URL.MQTT);

        mqttClient.on('connect', () => {
            setIsConnected(true);
            console.log('Connected to MQTT broker');
            mqttClient.subscribe(topic);
        });

        mqttClient.on('message', (topic, payload) => {
            setMessages((prevMessages) => [...prevMessages, payload.toString()]);
        });

        mqttClient.on('error', (err) => {
            mqttClient.end();
        });

        setClient(mqttClient);

        return () => {
            if (mqttClient) {
                mqttClient.end();
            }
        };
    }, [topic]);

    const publishMessage = (msg:string) => {
        if (client && isConnected) {
            client.publish(topic, msg);
        }
    };

    return {
        isConnected,
        messages,
        setMessages,
        publishMessage
    };
};

export default useMqtt;
