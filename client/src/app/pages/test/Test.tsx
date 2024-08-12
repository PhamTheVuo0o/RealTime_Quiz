import { useState } from 'react';
import { useSelector } from 'react-redux';
import { useMqtt } from '@ShareCores';
import { RootState } from '@ShareStores';
import { EventUtil } from '@ShareUtils';
export const Test = () => {

  const currentUserName = useSelector((state: RootState) => state.GlobalVar.currentUserName);

  const [count, setCount] = useState(1);
  const TOPIC = 'testtopic';
  const [message, setMessage] = useState('');
  const [id, setId] = useState('1');
  const { isConnected, messages, publishMessage } = useMqtt(TOPIC);

  EventUtil.useBackgroudJob(2, () => {
    const message = count > 1
      ? `${currentUserName}-${id} : Sent ${count} times`
      : `${currentUserName}-${id} : Sent ${count} time`;
    publishMessage(message);
    setCount(count + 1);
  });

  function publish(content:string){
    setCount(count + 1);
    const temp = `${currentUserName}-${id} : Sent ${content}`
      publishMessage(temp);
    const message = count > 1
      ? `${currentUserName}-${id} : Sent ${count} times`
      : `${currentUserName}-${id} : Sent ${count} time`;
    publishMessage(message);
  }

  const handlePublish = () => {
    if (message.length > 0){
      publish(message);
      setMessage('');
    }
  };

  return (
    <div>
      <h2>Test page</h2>
      {
        count>1 && (<p>Job executed {count-1} times</p>)
      }
      {
        count<=1 && (<p>Job executed {count-1} time</p>)
      }
      <div>
        <h1>MQTT Message</h1>
        <div>
          <input
            type="text"
            value={id}
            onChange={(e) => setId(e.target.value)}
            placeholder="Enter Id"
          />
          <input
            type="text"
            value={message}
            onChange={(e) => setMessage(e.target.value)}
            placeholder="Enter message"
          />
          <button onClick={handlePublish} disabled={!isConnected}>
            Publish
                </button>
        </div>
        <h2>Messages:</h2>
        <ul>
          {messages.slice(-30).reverse().map((msg, index) => (
            <li key={index}>{msg}</li>
          ))}
        </ul>
      </div>
    </div>
  );
};

export default Test;
