import React, {useEffect, useState} from 'react';
import { List, ListItem, ListItemText, Typography } from '@mui/material';

export interface RankItem {
  QuizId: string;
  UserFullName: string;
  Core: number;
  LastAnswerTime: number;
}

interface RankListProps {
  json: string; 
  currentUserName: string;
}

export const RankList: React.FC<RankListProps> = ({ json, currentUserName }) => {
  const [rankItems, setRankItems] = useState<RankItem[]>([]);

  useEffect(() => {
    try{
      const tempRankItems = JSON.parse(json);
      setRankItems(tempRankItems);
    } catch (error) {
    }
  }, [json]);

  return (
    <List>
      {rankItems && (rankItems.map((item, index) => (
        <ListItem
          key={index}
          style={{
            backgroundColor: item.UserFullName === currentUserName ? '#e0f7fa' : 'transparent',
          }}
        >
          <ListItemText
            primary={<Typography variant="body1">{item.UserFullName}</Typography>}
            secondary={<Typography variant="body2">Core: {item.Core} ; Answer Time: {item.LastAnswerTime} seconds</Typography>}
          />
        </ListItem>
      )))}
    </List>
  );
};

export default RankList;
