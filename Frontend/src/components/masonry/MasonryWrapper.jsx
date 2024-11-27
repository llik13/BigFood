import * as React from "react";
import Box from "@mui/material/Box";
import { styled } from "@mui/material/styles";
import Paper from "@mui/material/Paper";
import Masonry from "@mui/lab/Masonry";
import "./masonry-custom.css";

import defaultImages from "./default-items.json";

const heights = [416, 200, 200, 110, 120, 200, 200, 154];

const Item = styled(Paper)(({ theme }) => ({
  backgroundColor: "#ebebeb",
  borderRadius: "unset",
  ...theme.typography.body2,
  textAlign: "center",
  color: theme.palette.text.secondary,
  ...theme.applyStyles("dark", {
    backgroundColor: "#1A2027",
  }),
}));

export default function ItemsMasonry() {
  return (
    <Box sx={{ minHeight: 393 }}>
      <Masonry columns={4} spacing={2}>
        {defaultImages.map((item, index) => (
          <Item
            key={item.id}
            style={{ backgroundImage: `url(${item.src})` }}
            sx={{ height: heights[index] }}
            className="masonry-elem"
          ></Item>
        ))}
      </Masonry>
    </Box>
  );
}
