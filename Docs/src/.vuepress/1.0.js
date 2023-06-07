module.exports = [
  {
    title: "Getting Started",
    collapsable: false,
    children: ["introduction"],
  },
  {
    title: "Authentication",
    collapsable: false,
    children: prefix("authentication", ["authentication", "roles"]),
  },
];

function prefix(prefix, children) {
  return children.map((child) => `${prefix}/${child}`);
}
